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
    public class OAIClearConnectionCF : OAIConfirmation
    {
        public const string COMMAND = OAIClearConnection.CMD;

        public OAIClearConnectionCF(string[] parts) : base(parts) { }
        public OAIClearConnectionCF(byte[] bytes) : base(bytes) { }

        public new void Process()
        {
            OAIClearConnection cmd = (OAIClearConnection)Cmd;

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

            device.RemoveCall(cmd.Call());

            OAIAgentModel agent = OAIAgentsController.Relay().Peek(device.Agent);

            if (null == agent)
            {
                return;
            }

            agent.RemoveCall(cmd.Call());
        }
    }
}
