using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Queues
{
    public class OAIDebuggerQueue : OAIQueue<string>
    {
        private static OAIDebuggerQueue Instance = null;

        public static OAIDebuggerQueue Relay()
        {
            if (null == Instance)
            {
                Instance = new OAIDebuggerQueue();
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
