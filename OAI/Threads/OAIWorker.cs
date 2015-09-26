using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using OAI.Queues;
using OAI.Packets;
using OAI.Activity;
using OAI.Sequences;
using OAI.Factories;

namespace OAI.Threads
{
    public class OAIWorker
    {
        protected OAISequence Sequence;

        public OAIWorker(OAISequence sequence)
        {
            Sequence = sequence;
        }

        public void Run()
        {
            for (;;)
            {
                // Application has stopped so kill the thread!
                if (!OAIRunning.Active)
                {
                    return;
                }

                // Only process packets when we're connected!
                if (!OAIReadQueue.Relay().Available() || 
                    !Sequence.iConnected())
                {
                    Thread.Sleep(100);
                    continue;
                }

                OAIEvent evt = OAIReadQueue.Relay().Line;
                OAIEventProcessFactory.ProcessEvent(evt);
            }
        }
    }
}
