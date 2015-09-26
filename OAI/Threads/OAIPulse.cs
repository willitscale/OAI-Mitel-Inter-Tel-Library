using System;
using System.Threading;
// Application
using OAI.Packets;
using OAI.Queues;
using OAI.Sequences;
using OAI.Activity;
using OAI.Packets.Commands;

namespace OAI.Threads
{
    /**
     * 
     * Page 18 - System OAI Toolkit Specifications Manual - Issue 10.20 January 2011
     * 
     * The communications system defines a timer that dictates how often it must receive a valid
     * command (only the command ID is required). This timer is registered as soon as the connection
     * is programmed for System OAI. The attached computer must send a command within the
     * programmed period (default is every 4.5 minutes) to indicate that the application is still
     * functional. Even if a command is not required, the computer should send the No Operation
     * (_NO) command (see page 140) to indicate that it is running. If the attached computer fails to
     * send a command within the programmed time interval, the system cancels all active monitor
     * sessions on the connection, sends a Resync Response (RS) event (see page 85), reregisters
     * the timer, and waits for a valid command. It also sets its OAI parameters to default and
     * clears its CT Gateway parameters.
     */
    public class OAIPulse
    {
        private OAISequence Sequence;

        // 30 Second Delay
        public const int DELAY = 30;

        // Second delay for checking if active
        public const int SPLIT = 5;

        // Frequency of iterations between pulses
        public const int FREQUENCY = DELAY / SPLIT;

        public OAIPulse(OAISequence sequence)
        {
            Sequence = sequence;
        }

        public void Run()
        {
            // Doesn't leave the thread lingering for the full duration of the 
            // delay when the application is terminated
            int interval = 0;

            for (;;)
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

                // Only do a no operation pulse every 30 seoncds if the 
                // connection sequence has completed!
                if (interval >= DELAY && Sequence.iConnected())
                {
                    OAIWriteQueue.Relay().Line = new OAINoOperation();
                    interval = 0;
                }
            }
        }
    }
}
