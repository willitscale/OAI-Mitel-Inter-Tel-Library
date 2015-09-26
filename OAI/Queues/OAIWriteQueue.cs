using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OAI.Packets;
using OAI.Purgatory;

namespace OAI.Queues
{
    public class OAIWriteQueue : OAIPacketQueue<OAICommand>
    {
        private static OAIWriteQueue Instance = null;

        public static OAIWriteQueue Relay()
        {
            if( null == Instance )
            {
                Instance = new OAIWriteQueue();
            }

            return Instance;
        }

        protected override bool Delay()
        {
            return true;
        }

        protected override bool Delayed()
        {
            if (0 < Events.Count)
            {
                return Events.First().Delayed();
            }
            return false;
        }
    }
}
