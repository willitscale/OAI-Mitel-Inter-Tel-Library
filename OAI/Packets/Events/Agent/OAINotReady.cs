using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Controllers;

namespace OAI.Packets.Events.Agent
{
    public class OAINotReady : OAIAgentEvent
    {
        /**
         * Not Ready – NRY
         */
        public const string EVENT = "NRY";

        public OAINotReady(string[] parts) : base(parts) { }
        public OAINotReady(byte[] bytes) : base(bytes) { }

        public new void Process()
        {
            SetAvailable(-1);
        }
    }
}
