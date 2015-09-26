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
     * Transfer Call (One Step) – _TO
     * 
     * Transfers a call that is presently connected at the 
     * <Affected_Ext>, thus moving the call to another device 
     * or to an outside phone
     */
    public class OAITransferCallOneStep : OAICommand
    {
        public const string CMD = "_TO";
        

        public OAITransferCallOneStep(string extension, string call, string destination)
        {
            Command = CMD;

            // _TO,<InvokeID>,<Affected_Ext>,<Call_ID>,<Destination_Number>,
            // <Mailbox_Number>,<Station_For_Toll_Restrict>,<Override_DND>,
            // <Caller_ID_Number>,<Caller_ID_Name>
            Arguments = new string[8];

            // Affected_Ext: Indicates a phone (online), or a trunk device.
            Arguments[0] = extension;

            // Call_ID: Specifies the ID of the call to be transferred.
            Arguments[1] = call;

            // Destination_Number: Identifies the number of the transfer 
            // destination that will receive the call. If the application 
            // wishes to dial an outside number, it must specify a trunk 
            // access code followed by the outside number. This field is 
            // a digit string, which may be up to 48 digits and may include 
            // the values listed below in Table 49.
            //
            //  Value           Indication      Value               Indication
            //  0-9, *, #       Keypad Digits   - (Hyphen)          Ignored
            //  P               Pause           ( ) (Parentheses)   Ignored
            //  F               Hookflash       ; (Semicolon)       Ignored
            //  ! (Exclamation) SPCL Key        Space               Ignored
            Arguments[2] = destination;

            // Mailbox_Number: (Optional) Identifies the mailbox device 
            // that <Destination_Number> should use if its device supports 
            // mailboxes. 

            // Station_For_Toll_Restrict: (Optional) Indicates the station 
            // extension to use for toll restriction (if other than <Affected_Ext>). 
            // This station extension must be a station device. Some feature codes 
            // and extensions resolve to different devices depending on which
            // device dials the number. The station extension specified in this 
            // field is used when determining if the call should be routed to a 
            // specified destination. This station extension is also the device 
            // used to resolve the digit string in <Destination_Dev> to an actual
            // destination.

            // Override_DND: (Optional) Indicates whether or not the call should 
            // override DND if the <Affected_Ext> is in DND (0 = Do not Override; 
            // 1 = Override). If blank, the default value of Do not Override (0) is used.

            // Caller_ID_Number: A digit string of up to 24 digits.

            // Caller_ID_Name: An ASCII string of up to 32 characters delimited 
            // by |'s. For example, |Doe, John J|.

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
