using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Packets.Commands;
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
            string call = CallID();

            OAICallModel model = GetCall(call);
            
            if (null == model)
            {
                model = new OAICallModel();
            }

            model.Extension = AffectedExt();
            model.Call = call;

            OAIDeviceModel device = GetDevice(model.Extension);

            if (null == device)
            {
                return;
            }

            device.AddCall(call);

            OAIAgentModel agent = GetAgent(device.Agent);

            if (null != agent)
            {
                model.Agent = agent.Agent;
                agent.AddCall(call);
            }

            OAICallsController.Relay().Push(call, model);
        }
    }
}
