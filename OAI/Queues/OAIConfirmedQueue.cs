using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OAI.Packets;

namespace OAI.Queues
{
    public class OAIConfirmedQueue : OAIQueue<OAICommand>
    {
        private static OAIConfirmedQueue Instance = null;

        public static OAIConfirmedQueue Relay()
        {
            if (null == Instance)
            {
                Instance = new OAIConfirmedQueue();
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
