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
     * Clear Connection – _CX
     * 
     * Terminates a connection to a call, disconnecting the specified device 
     * from the designated call. The <Affected_Ext> must have a local connection 
     * state of CONNECTED (i.e., the <Affected_Ext> must be in a state where 
     * manually hanging up would terminate the connection). On a normal two-party 
     * call, this performs the same action as the party hanging up. This command, 
     * however, cannot always force a station to go on-hook, allowing the station 
     * to remain in the off-hook idle state and listen to dialtone. On a multi-party 
     * conference, this drops the specified device and leaves the other parties 
     * in the conference connected, if appropriate.
     */
    public class OAIClearConnection : OAICommand
    {
        public const string CMD = "_CX";

        public OAIClearConnection(string extension, string call) : this(extension,call,false) {}

        public OAIClearConnection(string extension, string call, bool transfer)
        {
            Command = CMD;

            Arguments = new string[3];

            // Affected_Ext: Indicates a phone (off-hook), 
            // modem (Axxess only), or trunk.
            Arguments[0] = extension;

            Arguments[1] = call;

            // Complete_Transfer: (Optional Boolean field) Indicates whether 
            // or not disconnecting this call should complete a transfer. 
            // The call must be a transfer announce call, and the <Affected_Ext> 
            // must be a station for this to work.
            Arguments[2] = (transfer) ? "1" : "0";
        }

        public string Extension()
        {
            return Arguments[0];
        }

        public string Call()
        {
            return Arguments[1];
        }

        public string Transfer()
        {
            return Arguments[2];
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
