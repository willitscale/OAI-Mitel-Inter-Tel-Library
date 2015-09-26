using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Packets.Delays;

namespace OAI.Packets.Commands
{
    /**
     * Reconnect Call – _RC
     * 
     * Combines the Clear Connection (_CX) and the Retrieve Call (_RV) commands 
     * (see pages 107 and 178, respectively). This clears an existing connection 
     * and then retrieves a previously held connection at the same device.
     */
    public class OAIReconnectCall : OAICommand
    {
        public const string CMD = "_RC";

        public OAIReconnectCall(string extension, string call, string held)
        {
            Command = CMD;

            // _RC,<InvokeID>,<Affected_Ext>,<Call_to_Clear>,<Held_Call>
            Arguments = new string[3];

            // Affected_Ext: Indicates a phone or modem (Axxess only).
            Arguments[0] = extension;

            // Call_To_Clear: Identifies the active call. This is the call that will be cleared.
            Arguments[1] = call;

            // Held_Call: Indicates the holding call. This is the call that will be retrieved.
            Arguments[2] = held;
        }

        public string Extension()
        {
            return Arguments[0];
        }

        public string Call()
        {
            return Arguments[1];
        }

        public string Held()
        {
            return Arguments[2];
        }

        public override bool Delayed()
        {
            return OAIDeviceDelay.Relay().Delayed(Extension()) ||
                OAICallDelay.Relay().Delayed(Call()) ||
                OAICallDelay.Relay().Delayed(Held());
        }

        public override void Block()
        {
            OAIDeviceDelay.Relay()
                .Block(Extension());

            OAICallDelay.Relay()
                .Block(Call());

            OAICallDelay.Relay()
                .Block(Held());
        }

        public override void Release()
        {
            OAIDeviceDelay.Relay()
                .Release(Extension());

            OAICallDelay.Relay()
                .Release(Call());

            OAICallDelay.Relay()
                .Release(Held());
        }
    }
}
