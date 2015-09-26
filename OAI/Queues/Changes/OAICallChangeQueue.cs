using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Queues.Changes
{
    public class OAICallChangeQueue : OAIQueue<string>
    {
        private static OAICallChangeQueue Instance = null;

        public static OAICallChangeQueue Relay()
        {
            if (null == Instance)
            {
                Instance = new OAICallChangeQueue();
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
