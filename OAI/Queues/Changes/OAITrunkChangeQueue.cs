using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Queues.Changes
{
    class OAITrunkChangeQueue : OAIQueue<string>
    {
        private static OAITrunkChangeQueue Instance = null;

        public static OAITrunkChangeQueue Relay()
        {
            if (null == Instance)
            {
                Instance = new OAITrunkChangeQueue();
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
