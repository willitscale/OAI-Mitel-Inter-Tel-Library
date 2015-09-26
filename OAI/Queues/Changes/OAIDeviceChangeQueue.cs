using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Queues.Changes
{
    public class OAIDeviceChangeQueue : OAIQueue<string>
    {
        private static OAIDeviceChangeQueue Instance = null;

        public static OAIDeviceChangeQueue Relay()
        {
            if (null == Instance)
            {
                Instance = new OAIDeviceChangeQueue();
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
