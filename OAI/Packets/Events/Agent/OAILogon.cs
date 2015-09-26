using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Controllers;
using OAI.Queues;
using OAI.Models;

namespace OAI.Packets.Events.Agent
{
    public class OAILogon : OAIAgentEvent
    {
        /**
         * Logon – LN
         */
        public const string EVENT = "LN";

        public OAILogon(string[] parts) : base(parts) { }
        public OAILogon(byte[] bytes) : base(bytes) { }

        public new void Process()
        {
            // Set the agent with the logged in extension
            SetAgent(Agent(), Extension());

            // Set the extension with the logged in agent
            SetExtension(Extension(), Agent());

            // Set Agent as available
            SetAvailable(1);

            // Add logged in hunt group
            AddHuntGroup(HuntGroup());
        }
    }
}
