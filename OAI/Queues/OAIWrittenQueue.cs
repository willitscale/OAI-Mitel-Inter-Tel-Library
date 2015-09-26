using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OAI.Packets;

namespace OAI.Queues
{
    public class OAIWrittenQueue : OAIPacketQueue<OAICommand>
    {
        private static OAIWrittenQueue Instance = null;

        public static OAIWrittenQueue Relay()
        {
            if (null == Instance)
            {
                Instance = new OAIWrittenQueue();
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
