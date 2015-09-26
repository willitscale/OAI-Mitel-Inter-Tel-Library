using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Packets.Commands;
using OAI.Controllers;
using OAI.Models;

namespace OAI.Packets.Events.Commands
{
    class OAIRetrieveCallCF : OAIConfirmation
    {
        public const string COMMAND = OAIReconnectCall.CMD;

        public OAIRetrieveCallCF(string[] parts) : base(parts) { }
        public OAIRetrieveCallCF(byte[] bytes) : base(bytes) { }


        // <Sequence_Number>,CF,_RV,<InvokeID>,<Outcome>
        public new void Process()
        {
            OAIRetrieveCall cmd = (OAIRetrieveCall)Cmd;

            if (null == cmd)
            {
                return;
            }

            // Command failed
            if (0 != Outcome())
            {
                return;
            }

            OAIDeviceModel device = OAIDevicesController.Relay().Peek(cmd.Extension());

            if (null == device)
            {
                return;
            }

            device.ActiveCall = cmd.Call();

            OAIAgentModel agent = OAIAgentsController.Relay().Peek(device.Agent);

            if (null == agent)
            {
                return;
            }

            agent.ActiveCall = cmd.Call();
        }
    }
}
