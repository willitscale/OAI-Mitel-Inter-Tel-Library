using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Queues.Changes
{
    public class OAINodeChangeQueue : OAIQueue<string>
    {
        private static OAINodeChangeQueue Instance = null;

        public static OAINodeChangeQueue Relay()
        {
            if (null == Instance)
            {
                Instance = new OAINodeChangeQueue();
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
