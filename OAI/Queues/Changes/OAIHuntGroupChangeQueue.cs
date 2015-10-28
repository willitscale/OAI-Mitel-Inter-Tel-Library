using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Queues.Changes
{
    public class OAIHuntGroupChangeQueue : OAIQueue<string>
    {
        private static OAIHuntGroupChangeQueue Instance = null;

        public static OAIHuntGroupChangeQueue Relay()
        {
            if (null == Instance)
            {
                Instance = new OAIHuntGroupChangeQueue();
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
