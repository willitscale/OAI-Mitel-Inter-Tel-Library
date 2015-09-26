using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Packets.Commands;

namespace OAI.Packets.Events.Commands
{
    public class OAITransferCallCF : OAIConfirmation
    {
        public const string COMMAND = OAITransferCall.CMD;

        public OAITransferCallCF(string[] parts) : base(parts) { }
        public OAITransferCallCF(byte[] bytes) : base(bytes) { }

        public new void Process()
        {
            // Command failed
            if (0 != Outcome())
            {
                return;
            }
        }

        /**
         * 5 <New_Call_ID>
         * Specifies the Call ID of the resulting call.
         */
        public string NewCallID()
        {
            return Part(5);
        }

        /**
         * 6 <Destination_Ext>
         * Indicates the transfer destination extension (internal 
         * extension). If transferred to the public network, this 
         * is the extension of the trunk used.
         */
        public string DestinationExt()
        {
            return Part(6);
        }

        /**
         * 7 <Transferred_Ext>
         * Identifies the device (internal extension) that was 
         * transferred. This field may be blank if the transferred 
         * call is a conference call.
         */
        public string TransferredExt()
        {
            return Part(7);
        }

        /**
         * 8 <Outside_Caller_Name>
         * Indicates the outside caller’s name of the transferred 
         * device (e.g., Caller ID/ANI name, if available). This 
         * information can be added/modified with the Modify Call 
         * (_MD) command in System OAI but cannot be deleted. This 
         * field is always blank for IC calls.
         */
        public string OutsideCallerName()
        {
            return Part(8);
        }

        /**
         * 9 <Outside_Caller_Number>
         * Identifies the outside caller’s Caller ID/ANI number 
         * for an incoming CO call, if available. This can be 
         * added/modified with the Modify Call (_MD) command in 
         * System OAI but cannot be deleted. This field is always 
         * blank for IC calls.
         */
        public string OutsideCallerNumber()
        {
            return Part(9);
        }

        /**
         * 10 <Trunk_Name>
         * Specifies the name associated with the transferred 
         * device if it is a trunk (e.g., the name programmed in 
         * the system’s call routing table for the incoming number
         * dialed [DID/DNIS]). If there is no DID/DNIS number, this 
         * field displays the trunk description programmed in the 
         * communications system. This information can be added/modified 
         * with the Modify Call (_MD) command is System OAI but 
         * cannot be deleted. This field is always blank for IC calls.
         */
        public string TrunkName()
        {
            return Part(10);
        }

        /**
         * 11 <Trunk_Outside_Number>
         * Identifies the DNIS or DID information associated with the 
         * transferred trunk, if applicable. This can be added/modified 
         * with the Modify Call (_MD) command in System OAI but cannot 
         * be deleted. This field is always blank for IC calls.
         */
        public string TrunkOutsideNumber()
        {
            return Part(11);
        }
    }
}
