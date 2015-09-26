using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OAI.Packets;

namespace OAI.Queues
{
    public class OAIReadQueue : OAIPacketQueue<OAIEvent>
    {
        private static OAIReadQueue Instance = null;

        public static OAIReadQueue Relay()
        {
            if (null == Instance)
            {
                Instance = new OAIReadQueue();
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
