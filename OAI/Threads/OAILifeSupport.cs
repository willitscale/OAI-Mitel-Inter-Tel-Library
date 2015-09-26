using System;
using System.Threading;

using OAI.Activity;
using OAI.Sequences;
using OAI.Queues;
using OAI.Models;
using OAI.Controllers;
using OAI.Packets.Commands;
using OAI.Recovery;

namespace OAI.Threads
{
    public class OAILifeSupport
    {
        private OAISequence Sequence;

        // 30 Second Delay
        public const int DELAY = 30;

        // Second delay for checking if active
        public const int SPLIT = 5;

        // Frequency of iterations between pulses
        public const int FREQUENCY = DELAY / SPLIT;

        public OAILifeSupport(OAISequence sequence)
        {
            Sequence = sequence;
        }

        public void Run()
        {
            // Doesn't leave the thread lingering for the full duration of the 
            // delay when the application is terminated
            int interval = 0;

            for(;;)
            {
                if (!OAIRunning.Active)
                {
                    return;
                }

                try
                {
                    Thread.Sleep(FREQUENCY * 1000);
                    interval += FREQUENCY;
                }
                catch (Exception exception)
                {
                    OAIDebuggerQueue.Relay().Line = exception.Message;
                    OAIDebuggerQueue.Relay().Line = exception.StackTrace;
                }

                // Only do a node check every 30 seoncds if the connection
                // sequence has completed!
                if (interval >= DELAY && Sequence.iConnected())
                {
                    NodeCheck();
                    interval = 0;
                }

            }
        }

        public void NodeCheck()
        {
            bool up = true;

            foreach (OAINodeModel model in OAINodeController.Relay().All())
            {
                // One or more nodes are down
                if (0 == model.Status)
                {
                    up = false;
                    break;
                }
            }

            // All nodes are up
            if (OAIState.NodeDown && up)
            {
                OAIState.NodeDown = false;

                // As one or more nodes were previously down and
                // are now up we need to resynchronise the packets
                if (!OAIState.ResyncRequested)
                {
                    OAIWriteQueue.Relay().Line = new OAIResyncRequest();
                    OAIState.ResyncRequested = true;
                }
            }

            // One or more nodes have failed
            if (!OAIState.NodeDown && !up)
            {
                OAIState.NodeDown = true;
            }
        }
    }
}
