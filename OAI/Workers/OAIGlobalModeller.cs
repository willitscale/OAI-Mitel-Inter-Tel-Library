using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Bus;
using OAI.Controllers;

using OAI.Structures.Queries;
using OAI.Queues;
using OAI.Queues.Changes;

using OAI.Models;

namespace OAI.Workers
{
    public class OAIGlobalModeller : OAIModeller
    {
        public override void Build()
        {
            // Stop event queue spam
            OAIAgentChangeQueue.Relay().Disable();
            OAIDeviceChangeQueue.Relay().Disable();

            foreach( OAIQueryExtendedStation device in OAIDeviceBus.Relay().All() )
            {
                OAIDeviceModel model = new OAIDeviceModel();

                model.Description = device.Description;
                model.Type = device.DeviceType();
                model.Extension = device.Extension;

                OAIDevicesController.Relay().Push(device.ID(),model);
            }

            foreach( OAIQueryExtendedAgent agent in OAIAgentBus.Relay().All() )
            {
                OAIAgentModel model = new OAIAgentModel();

                model.Agent = agent.Agent_ID;
                model.Extension = agent.Extension;
                model.Username = agent.Username;

                OAIAgentsController.Relay().Push(agent.ID(), model);
            }

            foreach( OAIQueryExtendedHuntGroup group in OAIHuntGroupBus.Relay().All() )
            {
                OAIHuntGroupModel model = new OAIHuntGroupModel();
                
                OAIHuntGroupsController.Relay().Push(group.ID(), model);
            }

            /**
             * 
             */
            foreach( OAIQQueryHuntGroup agent in OAIAgentHuntGroupBus.Relay().All() )
            {
                if (null != agent.Device_Ext && 0 < agent.Device_Ext.Length && 
                    OAIDevicesController.Relay().Exists(agent.Device_Ext))
                {
                    OAIDeviceModel model = OAIDevicesController.Relay().Peek(agent.Device_Ext);

                    model.Available = 0;

                    if (null != agent.Agent_ID && 0 < agent.Agent_ID.Length)
                    {
                        model.Agent = agent.Agent_ID;
                        model.Available = agent.Available() ? 1 : -1;
                    }
                }

                if (null != agent.Agent_ID && 0 < agent.Agent_ID.Length && 
                    OAIAgentsController.Relay().Exists(agent.Agent_ID))
                {
                    OAIAgentModel model = OAIAgentsController.Relay().Peek(agent.Agent_ID);

                    model.Available = 0;

                    if (null != agent.Device_Ext && 0 < agent.Device_Ext.Length)
                    {
                        model.Extension = agent.Device_Ext;
                        model.Available = agent.Available() ? 1 : -1;
                    }
                }
            }

            OAIAgentChangeQueue.Relay().Enable();
            OAIDeviceChangeQueue.Relay().Enable();
        }
    }
}
