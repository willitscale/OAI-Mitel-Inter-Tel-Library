using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Packets.Delays;

namespace OAI.Packets.Commands
{
    /**
     * Hold Call – _HC
     * 
     * Puts a call that is in the active (talking) state on hold at a 
     * specified device. The associated resources for the held call 
     * are made available for other uses, depending on <Reserve_Connection>.
     */
    public class OAIHoldCall : OAICommand
    {
        public const string CMD = "_HC";

        public OAIHoldCall(string extension, string call)
        {
            // _HC,<InvokeID>,<Affected_Ext>,<Call_ID>
            Arguments = new string[2];

            HoldBase(extension, call);
        }

        public OAIHoldCall(string extension, string call, bool reserve,
            bool dialtone, int type )
        {
            // _HC,<InvokeID>,<Affected_Ext>,<Call_ID>,<Reserve_Connection>,
            // <Leave_At_Dialtone>,<Hold_Type>
            Arguments = new string[5];

            HoldBase(extension, call);

            // Reserve_Connection: (Reserved for future use and is currently ignored.)
            Arguments[2] = (reserve) ? "1" : "0";

            // Leave_At_Dialtone: (Optional) Determines whether or not the station specified by
            // <Affected_Ext> will attempt to return to on-hook idle or remain at dialtone (0 = Attempt to
            // Return to Idle; 1 = Leave at Dialtone). If the station device is physically off-hook, this
            // command will not be able to force the station on-hook; it will have to leave the station at
            // dialtone. The default value is Leave at Dialtone (1).
            Arguments[3] = (dialtone) ? "1" : "0";

            // Hold_Type: (Optional) Describes the type of hold used for a call (0 = Individual Hold; 1 =
            // System Hold). The default value is Individual Hold (0).
            Arguments[4] = type.ToString();
        }

        protected void HoldBase(string extension, string call)
        {
            Command = CMD;

            // Affected_Ext: Indicates a phone (online)
            Arguments[0] = extension;

            Arguments[1] = call;
        }

        public string Extension()
        {
            return Arguments[0];
        }

        public string Call()
        {
            return Arguments[1];
        }

        public override bool Delayed()
        {
            return OAIDeviceDelay.Relay().Delayed(Extension()) ||
                OAICallDelay.Relay().Delayed(Call());
        }

        public override void Block()
        {
            OAIDeviceDelay.Relay()
                .Block(Extension());

            OAICallDelay.Relay()
                .Block(Call());
        }

        public override void Release()
        {
            OAIDeviceDelay.Relay()
                .Release(Extension());

            OAICallDelay.Relay()
                .Release(Call());
        }
    }
}
