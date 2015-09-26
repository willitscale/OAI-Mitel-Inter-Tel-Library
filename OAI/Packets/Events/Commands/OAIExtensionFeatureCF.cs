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
    class OAIExtensionFeatureCF : OAIConfirmation
    {
        public const string COMMAND = OAIExtensionFeature.CMD;

        public OAIExtensionFeatureCF(string[] parts) : base(parts) { }
        public OAIExtensionFeatureCF(byte[] bytes) : base(bytes) { }

        public new void Process()
        {
            OAIExtensionFeature cmd = (OAIExtensionFeature)Cmd;

            if (null == cmd)
            {
                return;
            }

            // Command failed
            if (0 != Outcome())
            {
                return;
            }
        }
    }
}
