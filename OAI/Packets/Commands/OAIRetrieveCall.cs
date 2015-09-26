using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Packets.Delays;

namespace OAI.Packets.Commands
{
    /**
     * Retrieve Call – _RV
     * 
     * Causes the specified extension to connect to a call that it previously 
     * put on hold.
     */
    public class OAIRetrieveCall : OAICommand
    {
        public OAIRetrieveCall(string extension, string call)
        {
            Command = "_RV";
            
            // _RV,<InvokeID>,<Affected_Ext>,<Call_ID>
            Arguments = new string[2];
            
            // Where valid device types for <Affected_Ext> are phones (online) 
            // or modems (Axxess only).
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
