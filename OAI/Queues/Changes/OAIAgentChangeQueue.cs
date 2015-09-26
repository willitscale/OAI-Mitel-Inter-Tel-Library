using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Queues.Changes
{
    public class OAIAgentChangeQueue : OAIQueue<string>
    {
        private static OAIAgentChangeQueue Instance = null;

        public static OAIAgentChangeQueue Relay()
        {
            if (null == Instance)
            {
                Instance = new OAIAgentChangeQueue();
            }

            return Instance;
        }

        protected override bool Delay()
        {
            return false;
        }

        protected override bool Delayed()
        {
            return false;
        }
    }
}
