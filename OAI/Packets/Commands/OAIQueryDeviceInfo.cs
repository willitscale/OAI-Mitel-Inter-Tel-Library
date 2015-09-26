using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Packets.Delays;

namespace OAI.Packets.Commands
{
    /**
     * Query Device Info – _QI
     * 
     * Queries general information about a device. The confirmation event for 
     * this service includes information on the class and type of the device 
     * being queried. An application can use the _QI command to obtain the 
     * station’s Account Code Type and Account Code Validated values. This
     * is used by the application to prompt a user for an account code when 
     * making an outgoing CO call.
     */
    class OAIQueryDeviceInfo : OAICommand
    {
        public const string CMD = "_QI";

        public OAIQueryDeviceInfo(string extension)
        {
            Command = CMD;

            // _QI,<InvokeID>,<Affected_Ext>
            Arguments = new string[1];

            Arguments[0] = extension;
        }

        public string Extension()
        {
            return Arguments[0];
        }

        public override bool Delayed()
        {
            return OAIDeviceDelay.Relay().Delayed(Extension());
        }

        public override void Block()
        {
            OAIDeviceDelay.Relay()
                .Block(Extension());
        }

        public override void Release()
        {
            OAIDeviceDelay.Relay()
                .Release(Extension());
        }
    }
}
