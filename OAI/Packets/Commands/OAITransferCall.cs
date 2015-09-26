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
     * Transfer Call – _TR
     * 
     * Transfers a held call to an active call when the <Affected_Ext> is 
     * involved with both calls. The announcement call is the transfer-
     * announcement call, and the held call is the call on hold (individual 
     * hold or transfer hold).
     */
    class OAITransferCall : OAICommand
    {
        public const string CMD = "_TR";

        public OAITransferCall(string extension, string held, string call) : 
            this(extension,held,call,false) {}

        public OAITransferCall(string extension, string held, 
            string call, bool hold)
        {
            Command = CMD;

            // _TR,<InvokeID>,<Affected_Ext>,<Held_Call_ID>,
            // <Announcement_Call_ID>,<Transfer_Type>
            Arguments = new string[4];

            // Affected_Ext: Indicates a phone (online).
            Arguments[0] = extension;

            // Held_Call_ID: Specifies the ID of the held call to be transferred.
            Arguments[1] = held;
            
            // Announcement_Call_ID: Indicates the ID of the announcement 
            // call that will receive the transferred call. This call can 
            // be in the alerting or connected state.
            Arguments[2] = call;

            // Transfer_Type: Indicates whether or not the call should 
            // automatically be placed on individual hold at the destination 
            // station (0 = Transfer To Ring; 1 = Transfer To Hold). Default is 
            // Transfer To Ring (0).
            Arguments[3] = ( hold ) ? "1" : "0";
        }

        public string Extension()
        {
            return Arguments[0];
        }

        public string Held()
        {
            return Arguments[1];
        }

        public string Call()
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
