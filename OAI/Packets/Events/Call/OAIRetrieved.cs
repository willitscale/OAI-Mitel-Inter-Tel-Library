using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Models;
using OAI.Controllers;
using OAI.Queues;
using OAI.Constants;

namespace OAI.Packets.Events.Call
{
    /**
     * Retrieved – RE
     * 
     * Occurs when a call that was held on a device is now being 
     * re-established at that device (i.e., the call is connected, 
     * in a talking state, again).
     * 
     * RE,<Resync_Code>,<Mon_Cross_Ref_ID>,<Call_ID>,<Retrieving_Ext>,
     * <Local_Cnx_State>,<Event_Cause>,<Internal_Retrieved_Ext>,
     * <Retrieved_Device_Outside_Number>,<Retrieved_Device_Trunk_Number>,
     * <Retrieved_Device_Type>
     */
    public class OAIRetrieved : OAICall
    {
        public const string EVENT = "RE";

        public OAIRetrieved(string[] parts) : base(parts) { }
        public OAIRetrieved(byte[] bytes) : base(bytes) { }

        /**
         * 5 - Retrieving_Ext
         * 
         * Identifies the device that retrieved the call from hold.
         */
        public string RetrievingExt()
        {
            return Part(5);
        }

        /**
         * 6 - Local_Cnx_State
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
            return Part(6);
        }

        /**
         * 7 - Event_Cause
         * 
         * This parameter provides extra information about the cause of the event. 
         * These cause values are encoded as two-digit decimal numbers.
         */
        public int EventCause()
        {
            return IntPart(7);
        }

        /**
         * Internal_Retrieved_Ext
         * 
         * Identifies the internal extension of the device that was on hold at
         * <Retrieving_Ext>. This field may be blank if the retrieved call is 
         * a conference call.
         */
        public string InternalRetrievedExt()
        {
            return Part(8);
        }

        /**
         * 9 - Retrieved_Device_Outside_Number
         * 
         * Indicates the outside caller’s Caller ID/ANI number for an 
         * incoming CO call, if available. This can be added/modified 
         * with the Modify Call (_MD) command but cannot be deleted. 
         * This field is always blank for IC calls.
         */
        public string RetrievedDeviceOutsideNumber()
        {
            return Part(9);
        }

        /**
         * 10 - Retrieved_Device_Trunk_Number
         * 
         * Identifies the outside number of the trunk used for the call 
         * (e.g., the number received in DNIS or DID information). This 
         * information can be added/modified with the Modify Call (_MD) 
         * command but cannot be deleted. This field is always blank for 
         * IC calls.
         */
        public string RetrievedDeviceTrunkNumber()
        {
            return Part(10);
        }

        /**
         * Retrieved_Device_Type
         * 
         * Indicates the type of device that is retrieving the call on 
         * hold (I = Internal Party; E = External Party).
         */
        public string RetrievedDeviceType()
        {
            return Part(11);
        }

        public new void Process()
        {
            // Set the call in the controller
            SetCall();

            AddCallToExtension(RetrievingExt());

            if (0 == OAICallingDeviceType.INTERNAL.CompareTo(RetrievedDeviceType()))
            {
                AddCallToExtension(InternalRetrievedExt());
            }
        }

        protected void SetCall()
        {
            string call = CallID();

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
                model.DDI = RetrievedDeviceTrunkNumber();
                model.CLI = RetrievedDeviceOutsideNumber();
                model.CNX = LocalCnxState();

                if (newCall)
                {
                    OAICallsController.Relay().Push(call, model);
                }
            }
        }
    }
}
