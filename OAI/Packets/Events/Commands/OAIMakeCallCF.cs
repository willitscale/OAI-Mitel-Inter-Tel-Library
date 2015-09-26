using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Packets.Commands;
using OAI.Structures.Queries;
using OAI.Bus;
using OAI.Models;
using OAI.Controllers;
using OAI.Queues;

namespace OAI.Packets.Events.Commands
{
    class OAIMakeCallCF : OAIConfirmation
    {
        public const string COMMAND = OAIMakeCall.CMD;

        public OAIMakeCallCF(string[] parts) : base(parts) { }
        public OAIMakeCallCF(byte[] bytes) : base(bytes) { }

        /**
         * <Call_ID>
         */
        public string CallID()
        {
            return Part(5);
        }

        /**
         * <Affected_Ext>
         */
        public string AffectedExt()
        {
            return Part(6);
        }

        public new void Process()
        {
            OAICallQuery entity = new OAICallQuery();
            OAICallModel model = new OAICallModel();

            model.Extension = entity.Affected_Ext = AffectedExt();
            model.Call = entity.Call_ID = CallID();

            OAIDeviceModel device = OAIDevicesController.Relay().Peek(entity.Affected_Ext);

            if (null == device)
            {
                return;
            }

            device.AddCall(entity.Call_ID);

            OAIAgentModel agent = OAIAgentsController.Relay().Peek(device.Agent);

            if (null != agent)
            {
                model.Agent = agent.Agent;
                agent.AddCall(entity.Call_ID);
            }

            OAICallBus.Relay().Push(entity);
            OAICallsController.Relay().Push(entity.Call_ID, model);
        }
    }
}
