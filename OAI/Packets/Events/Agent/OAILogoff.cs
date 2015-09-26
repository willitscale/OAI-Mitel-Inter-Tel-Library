using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Queues;

namespace OAI.Packets.Events.Agent
{
    public class OAILogoff : OAIAgentEvent
    {
        /**
         * Logoff – LF
         * 
         * Occurs when an agent logs out of an ACD hunt group. In protocol 
         * versions prior to 04.30, the communications system generates this 
         * event only under a Device-type monitor for the station where the 
         * ACD agent logged out. In protocol versions 04.30 and later, the 
         * communications system generates this event under a Device-type 
         * monitor for the ACD group where the ACD agent logged out (i.e., 
         * you can monitor the group instead of only the individual stations). 
         * 
         * LF,<Resync_Code>,<Mon_Cross_Ref_ID>,<Device_Ext>,<Agent_ID>,
         * <Agent_Description>,<ACD_Hunt_Group>
         */
        public const string EVENT = "LF";

        public OAILogoff(string[] parts) : base(parts) { }
        public OAILogoff(byte[] bytes) : base(bytes) { }

        public new void Process()
        {
            // Remove the agent from the extension being logged out
            SetAgent(Agent(), null);

            // Remove the extension of the logging out agent
            SetExtension(Extension(), null);

            // Remove logged in hunt group
            RemoveHuntGroup(HuntGroup());
        }
    }
}
