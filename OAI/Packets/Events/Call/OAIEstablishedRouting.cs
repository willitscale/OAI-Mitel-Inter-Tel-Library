using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Models;
using OAI.Controllers;
using OAI.Constants;

namespace OAI.Packets.Events.Call
{
    /**
     * Established Routing – ER
     * 
     * Occurs when the <Answering_Outside_Number> answers or connects 
     * to the specified call but before the end-user answers the call 
     * via human answer-supervision. At this point the call is not connected 
     * to the calling party, that is, in a talking state. The call is 
     * connected with the external destination for human answer supervision. 
     * This event is also generated when a Resync Request (RR or _RR) is sent 
     * specifically to a trunk device that is connected on a routing call
     * waiting for human answer supervision.
     */
    public class OAIEstablishedRouting : OAICall
    {
        public const string EVENT = "ER";

        protected OAICallModel CallModel;

        public OAIEstablishedRouting(string[] parts) : base(parts) { }
        public OAIEstablishedRouting(byte[] bytes) : base(bytes) { }

        /**
         * 5 - Answering_Internal_Ext
         * 
         * Identifies the internal extension of the device answering
         * (connecting) the call.
         */
        public string AnsweringInternalExt()
        {
            return Part(5);
        }

        /**
         * 6 - Answering_Outside_Number
         * 
         * Indicates the dialed digit string for an outgoing CO call;
         * otherwise, this is blank. This will eventually be the “connected number” 
         * parameter from the ISDN Connect message for PRI facilities.
         */
        public string AnsweringOutsideNumber()
        {
            return Part(6);
        }

        /**
         * 7 - Answering_Device_Type
         * 
         * Indicates the type of the device being alerted (I = Internal Party;
         * E = External Party).
         */
        public string AnsweringDeviceType()
        {
            return Part(7);
        }

        /**
         * 8 - Internal_Calling_Ext
         * 
         * Identifies the extension of the internal originator of the call. For
         * internal calls and outgoing CO calls, this is the calling party’s 
         * extension. For incoming CO calls, this is the trunk extension.
         */
        public string InternalCallingExt()
        {
            return Part(8);
        }

        /**
         * 9 - Outside_Caller_Number
         * 
         * Identifies the Caller ID/ANI number for incoming CO calls. This
         * can be added/modified with the Modify Call (_MD) command but cannot 
         * be deleted. This field is always blank for IC calls.
         */
        public string OutsideCallerNumber()
        {
            return Part(9);
        }

        /**
         * 10 - Trunk_Outside_Number
         * 
         * Indicates the DNIS or DID outside number associated with the
         * trunk used for the call. This can be added/modified with the 
         * Modify Call (_MD) command but cannot be deleted. This field 
         * is always blank for IC calls.
         */
        public string TrunkOutsideNumber()
        {
            return Part(10);
        }

        /**
         * 11 - Calling_Device_Type
         * 
         * Indicates the type of calling device (I = Internal Party; 
         * E = External Party).
         */
        public string CallingDeviceType()
        {
            return Part(11);
        }

        /**
         * 12 - Originally_Called_Dev
         * 
         * Indicates the initial call destination if the call has never been
         * answered. For IC and incoming CO calls, this is the originally dialed 
         * extension. For outgoing CO calls, this is the dialed digit string. The 
         * initial transfer destination for a transferred call and the initial 
         * recall destination for a recalling call become the originally called device.
         */
        public string OriginallyCalledDev()
        {
            return Part(12);
        }

        /**
         * 13 - Last_Redirection_Ext
         * 
         * Identifies the extension number of the last device that redirected (forwarded, 
         * deflected, transferred, or recalled) the call, if applicable; otherwise, this 
         * is blank.
         */
        public string LastRedirectionExt()
        {
            return Part(13);
        }

        /**
         * 14 - <Local_Cnx_State>
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
            return Part(14);
        }

        /**
         * 15 - <Event_Cause>
         * 
         * This parameter provides extra information about the cause of the event. 
         * These cause values are encoded as two-digit decimal numbers.
         */
        public string EventCause()
        {
            return Part(15);
        }

        public new void Process()
        {
            // Set the call in the controller
            SetCall();


            // Bind the call to the appropriate devices/agents
            if (0 == OAICallingDeviceType.EXTERNAL.CompareTo(AnsweringDeviceType()))
            {
                if (0 == OAICallingDeviceType.INTERNAL.CompareTo(CallingDeviceType()))
                {
                    OutboundCall();
                }
                else
                {
                    ExternalCall();
                }
            }
            else
            {
                if (0 == OAICallingDeviceType.INTERNAL.CompareTo(CallingDeviceType()))
                {
                    InboundCall();
                }
                else
                {
                    InternalCall();
                }
            }

            // Add the call to the answering internal extension
            AddCallToExtension(AnsweringInternalExt());

            // Add the call to the internal calling extension
            AddCallToExtension(InternalCallingExt());
        }

        protected void SetCall()
        {
            string call = CallID();

            bool newCall = false;

            if (null != call &&
                0 < call.Length)
            {
                CallModel = GetCall(call);

                if (null == CallModel)
                {
                    newCall = true;
                    CallModel = new OAICallModel();
                }

                // TODO: AND THE REST??

                CallModel.Call = call;

                CallModel.DDI = TrunkOutsideNumber();
                CallModel.CNX = LocalCnxState();

                if (newCall)
                {
                    OAICallsController.Relay().Push(call, CallModel);
                }
            }
        }

        // External ----> Internal
        protected void InboundCall()
        {
            AddCallToExtension(AnsweringInternalExt());

            OAIDeviceModel device = GetDevice(AnsweringInternalExt());

            CallModel.Extension = AnsweringInternalExt();

            if (null != device)
            {
                CallModel.Agent = device.Agent;
            }

            CallModel.CLI = OutsideCallerNumber();
            CallModel.Direction = OAICallDirection.INBOUND;
        }

        // Internal ----> External
        protected void OutboundCall()
        {
            AddCallToExtension(InternalCallingExt());

            OAIDeviceModel device = GetDevice(InternalCallingExt());

            CallModel.Extension = InternalCallingExt();

            if (null != device)
            {
                CallModel.Agent = device.Agent;
            }

            CallModel.CLI = AnsweringOutsideNumber();
            CallModel.Direction = OAICallDirection.OUTBOUND;
        }

        protected void InternalCall()
        {

            OAIDeviceModel device = GetDevice(InternalCallingExt());

            CallModel.Extension = InternalCallingExt();

            if (null != device)
            {
                CallModel.Agent = device.Agent;
            }

            AddCallToExtension(InternalCallingExt());
            AddCallToExtension(AnsweringInternalExt());

            CallModel.Direction = OAICallDirection.INTERNAL;
        }

        protected void ExternalCall()
        {
            CallModel.Direction = OAICallDirection.EXTERNAL;
            // TODO: ???
        }
    }
}
