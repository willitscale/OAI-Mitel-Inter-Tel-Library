using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Queues.Changes
{
    public class OAIFeatureChangeQueue : OAIQueue<string>
    {
        private static OAIFeatureChangeQueue Instance = null;

        public static OAIFeatureChangeQueue Relay()
        {
            if (null == Instance)
            {
                Instance = new OAIFeatureChangeQueue();
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
