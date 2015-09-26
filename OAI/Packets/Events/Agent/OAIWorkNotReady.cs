using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Agent
{
    public class OAIWorkNotReady : OAIAgentEvent
    {
        /**
         * Work Not Ready – WNR
         * 
         * <Device_Ext>
         * Identifies the extension number where the agent is logged in.
         * 
         * <Agent_ID>
         * Indicates the ID of the agent moving into the Work Not Ready state.
         * 
         */
        public const string EVENT = "WNR";

        public OAIWorkNotReady(string[] parts) : base(parts) { }
        public OAIWorkNotReady(byte[] bytes) : base(bytes) { }

        public new void Process()
        {
            SetAvailable(-1);
        }
    }
}
