using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Models;
using OAI.Controllers;
using OAI.Queues;

namespace OAI.Packets.Events.Call
{
    /**
     * Transferred – TR
     * 
     * Occurs when a station transfers a call to another device. This does 
     * not include the reverse transfer feature (call pick-up).
     * 
     * TR,<Resync_Code>,<Mon_Cross_Ref_ID>,<Transferred_Call_ID>,
     * <Transferring_Ext>,<Transferred_Ext>,<Announcement_Call_ID>,
     * <Destination_Ext>,<Outside_Caller_Name>,<Outside_Caller_Number>,
     * <Trunk_Name>,<Trunk_Outside_Number>,<Local_Cnx_State>,
     * <Event_Cause>
     */
    public class OAITransferred : OAICall
    {
        public const string EVENT = "TR";

        public OAITransferred(string[] parts) : base(parts) { }
        public OAITransferred(byte[] bytes) : base(bytes) { }

        /**
         * 4 - Transferred_Call_ID
         * 
         * Specifies the <Call_ID> (see page 25) of the transferred call. For
         * supervised transfers, this is the <Call_ID> that the transferring 
         * extension put on transferhold.
         */
        public string TransferredCallID()
        {
            return Part(4);
        }

        /**
         * 5 - Transferring_Ext
         * 
         * Indicates the extension of the device performing the transfer.
         */
        public string TransferringExt()
        {
            return Part(5);
        }

        /**
         * 6 - Transferred_Ext
         * 
         * Specifies the extension of the device that was transferred to the
         * <Destination_Ext>. This may be blank if the transferred call is 
         * a conference call.
         */
        public string TransferredExt()
        {
            return Part(6);
        }

        /**
         * 7 - Announcement_Call_ID
         * 
         * Displays the <Call_ID> (see page 25) of the transfer announcement 
         * call. This field is blank if the transferred event is sent on behalf 
         * of the device being transferred because it never had any knowledge 
         * of the announcement call.
         */
        public string AnnouncementCallID()
        {
            return Part(7);
        }

        /**
         * 8 - Destination_Ext
         * 
         * Indicates the extension of the transfer destination device.
         */
        public string DestinationExt()
        {
            return Part(8);
        }

        /**
         * 9 - Outside_Caller_Name
         * 
         * Identifies the outside caller’s Caller ID/ANI name for the
         * transferred device. This can be added/modified with the 
         * Modify Call (_MD) command in the System OAI but cannot be 
         * deleted. This text field is always blank for IC calls.
         */
        public string OutsideCallerName()
        {
            return Part(9);
        }

        /**
         * 10 - Outside_Caller_Number
         * 
         * Indicates the outside caller’s Caller ID/ANI number, if available,
         * of the transferred device. This can be added/modified with the 
         * Modify Call (_MD) command in System OAI but cannot be deleted. 
         * This field is always blank for IC calls. 
         */
        public string OutsideCallerNumber()
        {
            return Part(10);
        }

        /**
         * 11 - Trunk_Name
         * 
         * Displays the name associated with the transferred trunk, if applicable 
         * (e.g., the name programmed in the system’s call routing table for the 
         * incoming number dialed [DNIS/DID]). If there is no DID/DNIS number, 
         * this field displays the trunk description programmed in the communications 
         * system. This can be added/modified with the Modify Call (_MD) command 
         * in the System OAI but cannot be deleted. This text field is always blank 
         * for IC calls.
         */
        public string TrunkName()
        {
            return Part(11);
        }

        /**
         * 12 - Trunk_Outside_Number
         * 
         * Indicates the DNIS or DID outside number associated with the transferred 
         * trunk, if applicable. This can be added/modified with the Modify Call (_MD)
         * command in the System OAI but cannot be deleted. This field is always blank 
         * for IC calls.
         */
        public string TrunkOutsideNumber()
        {
            return Part(12);
        }

        /**
         * 13 - Local_Cnx_State
         * 
         * This single-character parameter defines the local connection state of 
         * the call, relative to the device sending the event, after the event 
         * occurs. The following table provides the values and descriptions 
         * associated with this parameter.
         * 
         * Value    Meaning     Description
         * N        Null        No connection to the call
         * I        Initiate    Dialtone connection
         * A        Alerting    Call is ringing at the device
         * C        Connected   Call is active at the device
         * H        Hold        Call is on-hold at the device
         * F        Failed      Device is listening to reorder tones
         * Q        Queued      Call is camped on to the device
         */
        public string LocalCnxState()
        {
            return Part(13);
        }

        /**
         * 14 - Event_Cause
         * 
         * This parameter provides extra information about the cause of the event. 
         * These cause values are encoded as two-digit decimal numbers.
         */
        public int EventCause()
        {
            return IntPart(14);
        }

        public new void Process()
        {
            // Set the transferred call in the controller
            SetCall(TransferredCallID(), true);

            // Bind the call being transfereed to the transferring extension
            AddCallToExtension(TransferringExt(), TransferredCallID());

            string announcement = AnnouncementCallID();

            // Check to see if there was an announcement call
            if (null != announcement && 0 < announcement.Length)
            { 
                SetCall(AnnouncementCallID(), false);
            }
            
            // Bind the transferred call to the new extension
            AddCallToExtension(DestinationExt(), TransferredCallID());
        }

        protected void SetCall(string call, bool data)
        {
            bool newCall = false;

            if (null != call &&
                0 < call.Length)
            {
                OAICallModel model = GetCall(call);

                if (null == model)
                {
                    newCall = true;
                    model = new OAICallModel();
                }

                model.Call = call;

                if (data)
                {
                    model.CNX = LocalCnxState();
                    model.CLI = OutsideCallerNumber();
                    model.Trunk = TrunkName();
                    model.DDI = TrunkOutsideNumber();
                    model.Caller = OutsideCallerName();
                }

                if (newCall)
                {
                    OAICallsController.Relay().Push(call, model);
                }
            }
        }
    }
}
