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
     * Service Initiated – SI
     * 
     * Indicates that a device requested dialtone or that a callback is 
     * prompting the user to take a phone off hook.
     * 
     * SI,<Resync_Code>,<Mon_Cross_Ref_ID>,<Call_ID>,<Initiating_Ext>,
     * <Local_Cnx_State>,<Event_Cause>,<Callback_Target_Ext>
     */
    public class OAIServiceInitiated : OAICall
    {
        public const string EVENT = "SI";

        public OAIServiceInitiated(string[] parts) : base(parts) { }
        public OAIServiceInitiated(byte[] bytes) : base(bytes) { }

        /**
         * 5 - Initiating_Ext
         * 
         * Identifies the extension of the device initiating the service.
         */
        public string InitiatingExt()
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
         * 8 - Callback_Target_Ext
         * 
         * Indicates the device for which the <Initiating_Ext> requested a
         * callback. This field is populated only if <Event_Cause> is 
         * Callback (4).
         */
        public string CallbackTargetExt()
        {
            return Part(8);
        }

        public new void Process()
        {
            // Set the call in the controller
            SetCall();

            AddCallToExtension(InitiatingExt());

            if (OAIEventCause.CALL_BACK == EventCause())
            {

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
                model.CNX = LocalCnxState();
                model.Extension = InitiatingExt();

                OAIDeviceModel device = GetDevice(InitiatingExt());

                if (null != device)
                {
                    model.Agent = device.Agent;
                }

                if (newCall)
                {
                    OAICallsController.Relay().Push(call, model);
                }
            }
        }
    }
}
