using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Agent
{
    public class OAIReady : OAIAgentEvent
    {
        /**
         * Ready – RY
         * 
         * <Device_Ext>
         * Identifies the extension number where the agent is logged in.
         * 
         * <Agent_ID>
         * Indicates the ID of the agent moving into the Ready state.
         * 
         */
        public const string EVENT = "RY";

        public OAIReady(string[] parts) : base(parts) { }
        public OAIReady(byte[] bytes) : base(bytes) { }

        public new void Process()
        {
            SetAvailable(1);
        }
    }
}
