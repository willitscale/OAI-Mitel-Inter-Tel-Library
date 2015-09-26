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
     * Originated – OR
     * 
     * Informs the application that the communications system is attempting 
     * to establish a call as a result of a manual attempt or a completed request 
     * from the application. The application should check for additional events to 
     * determine the status of the call as it proceeds either through the communications 
     * system or out to a public network. This event is not sent when the device
     * initiates a feature.
     */
    public class OAIOriginated : OAICall
    {
        public const string EVENT = "OR";

        public OAIOriginated(string[] parts) : base(parts) { }
        public OAIOriginated(byte[] bytes) : base(bytes) { }

        /**
         * 5 - Internal_Calling_Ext
         * 
         * Identifies the extension of the internal originator of the call. For
         * internal calls and outgoing CO calls, this is the calling party’s 
         * extension. For incoming CO calls, this is the trunk extension.
         */
        public string InternalCallingExt()
        {
            return Part(5);
        }

        /**
         * 6 - Outside_Caller_Number
         * 
         * Identifies the outside caller’s Caller ID/ANI number for incoming 
         * CO calls. This can be added/modified with the Modify Call (_MD) 
         * command but cannot be deleted. This field is always blank for IC calls.
         */
        public string OutsideCallerNumber()
        {
            return Part(6);
        }

        /**
         * 7 - Calling_Device_Type
         * 
         * Indicates the type of the calling device (I = Internal Party; E =
         * External Party).
         */
        public string CallingDeviceType()
        {
            return Part(7);
        }

        /**
         * 8 - Dialed_Number
         * 
         * Identifies the number dialed to make the call (internal or external). 
         * For incoming CO calls, this is either the DNIS information or the 
         * internal extension number where the call is routed. For manual outgoing 
         * CO calls, this field is only the trunk access code, unless a speed-dial 
         * key or the Make Call (_MC) command was used to make the call, because 
         * the dialed digits are not collected before the call connects to the trunk.
         */
        public string DialedNumber()
        {
            return Part(8);
        }

        /**
         * 9 - Account Code
         * 
         * Indicates the account code used to make the call. This field is blank for IC
         * calls and CO calls that did not use an account code.
         */
        public string AccountCode()
        {
            return Part(9);
        }

        /**
         * 10 - Local_Cnx_State
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
            return Part(10);
        }

        /**
         * 11 - Event_Cause
         * 
         * This parameter provides extra information about the cause of the event. 
         * These cause values are encoded as two-digit decimal numbers.
         */
        public int EventCause()
        {
            return IntPart(10);
        }

        public new void Process()
        {
            // Set the call in the controller
            SetCall();

            AddCallToExtension(InternalCallingExt());
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

                if (0 == OAICallingDeviceType.EXTERNAL.CompareTo(CallingDeviceType()))
                {
                    model.CLI = DialedNumber();
                }
                else
                {
                    model.CLI = OutsideCallerNumber();
                }

                model.AccountCode = AccountCode();
                model.CNX = LocalCnxState();

                if (newCall)
                {
                    OAICallsController.Relay().Push(call, model);
                }
            }
        }
    }
}
