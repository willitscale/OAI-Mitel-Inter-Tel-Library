using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Models;
using OAI.Controllers;
using OAI.Queues;
using OAI.Packets.Delays;

namespace OAI.Packets.Commands
{
    /**
     * Clear Call – _CC
     * 
     * Terminates a call and all connections to that call. On a normal two-party 
     * call, this is equivalent to one party (<Affected_Ext>) hanging up. On a 
     * multi-party conference, this breaks up the conference and hangs up on 
     * all parties.
     */
    public class OAIClearCall : OAICommand
    {
        public const string CMD = "_CC";

        public OAIClearCall(string extension, string call)
        {
            Command = CMD;

            Arguments = new string[2];

            // Where all device types are valid for <Affected_Ext>, 
            // and the <Affected_Ext> must be involved in the call.
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
