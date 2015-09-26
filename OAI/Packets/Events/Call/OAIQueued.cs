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
     * Queued – QU
     * 
     * Indicates that a call has been delivered or redirected to a trunk 
     * group or hunt/ACD group, and the call queues (“camps on” in the 
     * Inter-Tel Axxess and 5000 CP). 
     * 
     * QU,<Resync_Code>,<Mon_Cross_Ref_ID>,<Call_ID>,<Queued_Ext>,
     * <Internal_Calling_Ext>,<Outside_Caller_Name>,<Outside_Caller_Number>,
     * <Trunk_Name>,<Trunk_Outside_Number>,<Calling_Device_Type>,
     * <Originally_Called_Dev>,<Last_Redirection_Ext>,<Number_Queued>,
     * <Account_Code>,<Local_Cnx_State>,<Event_Cause>
     */
    public class OAIQueued : OAICall
    {
        public const string EVENT = "QU";

        public OAIQueued(string[] parts) : base(parts) { }
        public OAIQueued(byte[] bytes) : base(bytes) { }

        /**
         * 5 - Queued_Ext
         * 
         * Indicates the device where the call is queued.
         */
        public string QueuedExt()
        {
            return Part(5);
        }

        /**
         * 6 - Internal_Calling_Ext
         * 
         * Indicates the extension number of the internal call originator. 
         * For internal calls and outgoing CO calls, this is the calling 
         * party’s extension. For incoming CO calls, this is the trunk extension.
         */
        public string InternalCallingExt()
        {
            return Part(6);
        }

        /**
         * 7 - Outside_Caller_Name
         * 
         * Identifies the Caller ID/ANI name for incoming CO calls. This can
         * be added/modified with the Modify Call (_MD) command but cannot be 
         * deleted. This text field is always blank for IC calls.
         */
        public string OutsideCallerName()
        {
            return Part(7);
        }

        /**
         * 8 - OutsideCallerNumber
         * 
         * Indicates the outside caller’s Caller ID/ANI number, if available.
         * This can be added/modified with the Modify Call (_MD) command but 
         * cannot be deleted. This field is always blank for IC calls.
         */
        public string OutsideCallerNumber()
        {
            return Part(8);
        }

        /**
         * 9 - Trunk_Name
         * 
         * Displays the name associated with the trunk used for the call (e.g., the
         * name programmed in the system’s call routing table for the incoming number 
         * dialed [DNIS/DID]). This can be added/modified with the Modify Call (_MD) 
         * command but cannot be deleted. This text field is always blank for IC calls.
         */
        public string TrunkName()
        {
            return Part(9);
        }

        /**
         * 10 - Trunk_Outside_Number
         * 
         * Indicates the DNIS or DID outside number associated with the trunk used 
         * for the call. This can be added/modified with the Modify Call (_MD) 
         * command but cannot be deleted. This field is always blank for IC calls.
         */
        public string TrunkOutsideNumber()
        {
            return Part(10);
        }

        /**
         * 11 - Calling_Device_Type
         * 
         * Indicates the type of the calling device (I = Internal Party; E = 
         * External Party).
         */
        public string CallingDeviceType()
        {
            return Part(11);
        }

        /**
         * 12 - Originally_Called_Dev
         * 
         * Indicates the initial call destination if the call has never been
         * answered. For IC and incoming CO calls, this is the originally 
         * dialed extension. For outgoing CO calls, this is the dialed digit 
         * string. The initial transfer destination for a transferred call 
         * and the initial recall destination for a recalling call become 
         * the originally called device.
         */
        public string OriginallyCalledDev()
        {
            return Part(12);
        }

        /**
         * 13 - Last_Redirection_Ext
         * 
         * Identifies the extension number of the last device that redirected
         * (forwarded, deflected, transferred, or recalled) the call, if applicable; 
         * otherwise, this is blank.
         */
        public string LastRedirectionExt()
        {
            return Part(13);
        }

        /**
         * 14 - Number_Queued
         * 
         * Indicates the call’s position in the queue (1-65535), where a “1”
         * indicates that the call is the first call waiting for an available 
         * device to ring.
         */
        public string NumberQueued()
        {
            return Part(14);
        }

        /**
         * 15 - AccountCode
         * 
         * Specifies the account code if Call Processing collected an account code
         * before the callback request occurred. If no account code was collected, 
         * this field is blank.
         */
        public string AccountCode()
        {
            return Part(15);
        }

        /**
         * 16 - Local_Cnx_State
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
            return Part(16);
        }

        /**
         * 17 - Event_Cause
         * 
         * Indicates why the event occurred. The only possible values for this 
         * field are as follows:
         * 
         * 23 = No available agents
         * 30 = Some resource is not available
         * 33 = Trunks busy
         * 40 = Transfer announcement (takes precedence over the other two)
         */
        public int EventCause()
        {
            return IntPart(11);
        }

        public new void Process()
        {
            // Set the call in the controller
            SetCall();

            // Don't directly add the call as it's not an active call
            QueueCallToExtension(QueuedExt());

            // If it's an internal call then this call should be the originator's active call
            if (0 == OAICallingDeviceType.INTERNAL.CompareTo(CallingDeviceType()))
            {
                AddCallToExtension(InternalCallingExt());
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
                model.AccountCode = AccountCode();

                if (0 == OAICallingDeviceType.EXTERNAL.CompareTo(CallingDeviceType()))
                {
                    model.Trunk = TrunkName();
                    model.CLI = OutsideCallerNumber();
                    model.DDI = TrunkOutsideNumber();
                }

                if (newCall)
                {
                    OAICallsController.Relay().Push(call, model);
                }
            }
        }
    }
}
