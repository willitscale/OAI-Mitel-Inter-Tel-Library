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
    public class OAIHoldCallCF : OAIConfirmation
    {
        public const string COMMAND = OAIHoldCall.CMD;

        public OAIHoldCallCF(string[] parts) : base(parts) { }
        public OAIHoldCallCF(byte[] bytes) : base(bytes) { }

        public new void Process()
        {
            OAIHoldCall cmd = (OAIHoldCall)Cmd;

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

            device.ActiveCall = null;

            OAIAgentModel agent = OAIAgentsController.Relay().Peek(device.Agent);

            if (null == agent)
            {
                return;
            }

            agent.ActiveCall = null;
        }
    }
}
