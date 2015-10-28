using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
// Added
using System.Net;
using System.Net.Sockets;
using System.IO;
// Application 
using OAI.Packets;
using OAI.Packets.Events;
using OAI.Queues;
using OAI.Purgatory;
using OAI.Sequences;
using OAI.Factories;
using OAI.Configuration;
using OAI.Tools;
using OAI.Activity;

namespace OAI.Threads
{
    public class OAISocket
    {
        // Worker
        protected OAIWorker Worker;
        protected Thread WorkerThread;

        // No Operation pulse emitter
        protected OAIPulse Pulse;
        protected Thread PulseThread;

        // Link failure life support monitor
        protected OAILifeSupport LifeSupport;
        protected Thread LifeSupportThread;

        protected TcpClient Client;
        protected NetworkStream Stream;
        protected OAIConfig Config;
        protected OAISequence Sequence;

        public OAISocket(OAIConfig config, OAISequence sequence)
        {
            Config = config;
            Sequence = sequence;
            
            Worker = new OAIWorker(Sequence);
            WorkerThread = new Thread(new ThreadStart(Worker.Run));

            Pulse = new OAIPulse(Sequence);
            PulseThread = new Thread(new ThreadStart(Pulse.Run));

            LifeSupport = new OAILifeSupport(Sequence);
            LifeSupportThread = new Thread(new ThreadStart(LifeSupport.Run));
        }

        public void Run()
        {
            OAIRunning.ActiveThreads++;
            try
            {
                Client = new TcpClient();

                Client.Connect(Config.Host, Config.Port);

                WorkerThread.Start();
                PulseThread.Start();
                LifeSupportThread.Start();

                Stream = Client.GetStream();

                byte[] buffer = OAIUtils.PBXConnection(Config.Type, 
                    Config.Name, Config.Password);

                Stream.Write(buffer, 0, buffer.Length);

                if (Stream.CanRead && Stream.CanWrite)
                {
                    for (;;)
                    {
                        if (!OAIRunning.Active)
                        {
                            OAIRunning.ActiveThreads--;
                            return;
                        }

                        // Only sleep the thread if there is no activity
                        if (!Stream.DataAvailable && 
                            !OAIWriteQueue.Relay().Available())
                        {
                            Thread.Sleep(50);
                            continue;
                        }

                        if (OAIWriteQueue.Relay().Available())
                        {
                            Write();
                        }
                        else
                        {
                            Read();
                        }
                    }
                }
            }
            catch(Exception exception)
            {
                OAIDebuggerQueue.Relay().Line = exception.Message;
                OAIDebuggerQueue.Relay().Line = exception.StackTrace;

                Client.Close();
            }
            finally
            {
                Thread.Sleep(2000);
                if (OAIRunning.Active)
                {
                    OAIRunning.ActiveThreads--;
                    Run();
                }
            }
        }

        protected void Read()
        {
            int bytesRead = 0;

            // Packet Header
            byte[] header = new byte[4];
            bytesRead = Stream.Read(header, 0, 4);

            // Duff Packet Header
            if (0 == bytesRead)
            {
                return;
            }

            int packetSize = OAIUtils.BigEndian(header);

            // Duff Packet Size
            if (0 == packetSize)
            {
                return;
            }

            // Packet Message
            byte[] buffer = new byte[packetSize];

            bytesRead = 0;
            // Handle the request as if it was chunked
            while (packetSize > (bytesRead += Stream.Read(buffer,
                bytesRead, packetSize - bytesRead))) { }

            OAIEvent evt = OAIEventFactory.Production(buffer);

            // Process confirmation packets differently
            if (evt.Confirmation())
            {
                OAICommandConfirmation.Confirmation(
                    (OAIConfirmation)evt);
            }
            else
            {
                OAIReadQueue.Relay().Line = evt;
            }

            if (!Sequence.iConnected())
            {
                Sequence.Step(evt);
            }

            // Something has happened to the connection
            if (0 == bytesRead)
            {
                return;
            }
        }

        protected void Write()
        {
            OAICommand command = OAIWriteQueue.Relay().Line;

            byte[] buffer = OAIUtils.PBXPacket(command);

            // Print the packet 
            OAIDebuggerQueue.Relay().Line = command.PacketMessage();

            Stream.Write(buffer, 0, buffer.Length);

            // Add the packet to the confirmation map
            OAICommandConfirmation.Push(command);

            // Packet has been written
            OAIWrittenQueue.Relay().Line = command;
        }
    }
}
