using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Queues.Changes
{
    public class OAIDNDChangeQueue : OAIQueue<string>
    {
        private static OAIDNDChangeQueue Instance = null;

        public static OAIDNDChangeQueue Relay()
        {
            if (null == Instance)
            {
                Instance = new OAIDNDChangeQueue();
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
