using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Models;
using OAI.Controllers;

namespace OAI.Packets.Events.Agent
{
    public abstract class OAIAgentEvent : OAIEvent
    {
        public OAIAgentEvent(string[] parts) : base(parts) { }
        public OAIAgentEvent(byte[] bytes) : base(bytes) { }

        /**
         * 3 - <Device_Ext>
         * Identifies the extension number where the agent logged out.
         */
        public string Extension()
        {
            return Part(4);
        }

        /**
         * 4 - <Agent_ID>
         * Indicates the ID of the agent logging out of the hunt group.
         */
        public string Agent()
        {
            return Part(5);
        }

        /**
         * 5 - <Agent_Description>
         * Specifies the description of the station of the agent. In the event that
         * the agent is an agent ID and not a station, this field contains the 
         * description of the station that logged in using the agent ID, and not 
         * the description associated with the agent ID itself.
         */
        public String Description()
        {
            return Part(6);
        }

        /**
         * 6 - <ACD_Hunt_Group>
         * Indicates the extension number of the ACD hunt group where the
         * agent is logging out.
         */
        public String HuntGroup()
        {
            return Part(7);
        }

        protected void SetAgent(string agent, string extension)
        {
            OAIAgentModel model = GetAgent(agent);

            if (null != model)
            {
                model.Extension = extension;
            }
        }

        protected void SetExtension(string extension, string agent)
        {
            OAIDeviceModel model = GetDevice(extension);

            if (null != model)
            {
                model.Agent = agent;
            }
        }

        protected void SetAvailable(int available)
        {
            OAIAgentModel agent = GetAgent(Agent());

            if (null != agent)
            {
                agent.Available = available;
            }

            OAIDeviceModel device = GetDevice(Extension());

            if (null != device)
            {
                device.Available = available;
            }
        }

        protected OAIDeviceModel GetDevice(string extension)
        {
            if (null != extension && 0 < extension.Length)
            {
                return OAIDevicesController
                    .Relay()
                    .Peek(extension);
            }

            return null;
        }

        protected OAIAgentModel GetAgent(string agent)
        {
            if (null != agent && 0 < agent.Length)
            {
                return OAIAgentsController
                    .Relay()
                    .Peek(agent);
            }

            return null;
        }
    }
}
