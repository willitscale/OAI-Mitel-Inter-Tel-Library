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
            string device = Extension();
            string agent = Agent();

            // Set the agent with the logged in extension
            SetAgent(agent, device);

            // Set the extension with the logged in agent
            SetExtension(device, agent);

            // Set Agent as available
            SetAvailable(1);

            string group = HuntGroup();

            // Add logged in hunt group
            AddHuntGroup(group);

            OAIHuntGroupModel model = OAIHuntGroupsController
                .Relay()
                .Peek(group);

            if (null == model)
            {
                model = new OAIHuntGroupModel();
            }

            model.AddAgent(agent);
            model.AddDevice(device);

            OAIHuntGroupsController
                .Relay()
                .Push(group, model);
        }

        protected void AddHuntGroup(string group)
        {
            OAIAgentModel agent = GetAgent(Agent());

            if (null != agent)
            {
                agent.RemoveGroup(HuntGroup());
            }

            OAIDeviceModel device = GetDevice(Extension());

            if (null != device)
            {
                device.RemoveGroup(HuntGroup());
            }

            OAIHuntGroupModel huntGroup = GetHuntGroup(group);

            if (null == group)
            {
                huntGroup = new OAIHuntGroupModel();
                huntGroup.HuntGroup = group;
            }

            huntGroup.AddAgent(Agent());
            huntGroup.AddDevice(Extension());

            OAIHuntGroupsController
                .Relay()
                .Push(group, huntGroup);
        }
    }
}
