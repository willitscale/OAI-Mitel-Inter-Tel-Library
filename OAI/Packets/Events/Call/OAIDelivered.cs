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
     * Delivered – DE
     * 
     * Indicates that a call is alerting (ringing) at a specific device. 
     * Multiple delivered events can be sent for the same call when more 
     * than one station is ringing for one call (e.g., multiple ring in
     * and all-ring hunt groups)
     * 
     * DE,<Resync_Code>,<Mon_Cross_Ref_ID>,<Call_ID>,<Alerting_Internal_Ext>,
     * <Alerting_Outside_Number>,<Alerting_Device_Type>,
     * <Internal_Calling_Ext>,<Outside_Caller_Name>,<Outside_Caller_Number>,
     * <Trunk_Name>,<Trunk_Outside_Number>,<Calling_Device_Type>,
     * <Originally_Called_Dev>,<Last_Redirection_Ext>,<Account_Code>,
     * <Local_Cnx_State>,<Event_Cause>,<ACD/UCD_Group>
     */
    public class OAIDelivered : OAICall
    {
        public const string EVENT = "DE";

        protected OAICallModel CallModel;

        public OAIDelivered(string[] parts) : base(parts) { }
        public OAIDelivered(byte[] bytes) : base(bytes) { }

        /**
         * 5 - Alerting_Internal_Ext
         * 
         * Identifies the internal extension being alerted. For internal and
         * incoming CO calls, this is the ringing party’s extension. For 
         * outgoing CO calls, this is the trunk extension.
         */
        public string AlertingInternalExt()
        {
            return Part(5);
        }

        /**
         * 6 - Alerting_Outside_Number
         * 
         * Indicates the dialed digit string for an outgoing CO call;
         * otherwise, this is blank.
         */
        public string AlertingOutsideNumber()
        {
            return Part(6);
        }

        /**
         * 7 - Alerting_Device_Type
         * 
         * Indicates the type of device being alerted (I = Internal Party; E =
         * External Party).
         */
        public string AlertingDeviceType()
        {
            return Part(7);
        }
        
        /**
         * 8 - Internal_Calling_Ext
         * 
         * Indicates the extension of the internal call originator. For internal
         * calls and outgoing CO calls, this is the calling party’s extension. 
         * For incoming CO calls, this is the trunk extension.
         */
        public string InternalCallingExt()
        {
            return Part(8);
        }

        /**
         * 9 - Outside_Caller_Name
         * 
         * Identifies the Caller ID/ANI name for incoming CO calls. This can
         * be added/modified with the Modify Call (_MD) command but cannot be deleted. This text
         * field is always blank for IC calls and is delimited by vertical slashes (|).
         */
        public string OutsideCallerName()
        {
            return Part(9);
        }

        /**
         * 10 - Outside_Caller_Number
         * 
         * Indicates the Caller ID/ANI number for incoming CO calls. This
         * can be added/modified with the Modify Call (_MD) command but 
         * cannot be deleted. This field is always blank for IC calls.
         */
        public string OutsideCallerNumber()
        {
            return Part(10);
        }

        /**
         * 11 - Trunk_Name
         * 
         * Displays the name associated with the trunk used for the call (e.g., the
         * name programmed in the system’s call routing table for the incoming number dialed
         * [DNIS/DID]). If there is no DID/DNIS number, this field displays the trunk description
         * programmed in the communications system. This can be added/modified with the Modify
         * Call (_MD) command but cannot be deleted. This text field is always blank for IC calls and
         * is delimited by vertical slashes (|).
         */
        public string TrunkName()
        {
            return Part(11);
        }

        /**
         * 12 - Trunk_Outside_Number
         * 
         * Identifies the DNIS or DID outside number associated with the
         * trunk used for the call. This can be added/modified with the Modify 
         * Call (_MD) command but cannot be deleted. This field is always 
         * blank for IC calls.
         */
        public string TrunkOutsideNumber()
        {
            return Part(12);
        }

        /** 
         * 13 - Calling_Device_Type
         * 
         * Indicates the type of calling device (I = Internal Party; 
         * E = External Party).
         */
        public string CallingDeviceType()
        {
            return Part(13);
        }

        /**
         * 14 - Originally_Called_Dev
         * 
         * Indicates the initial call destination if the call has never been
         * answered. For IC and incoming CO calls, this is the originally dialed 
         * extension. For outgoing CO calls, this is the dialed digit string. 
         * The initial transfer destination for a transferred call and the 
         * initial recall destination for a recalling call become the originally
         * called device.
         */
        public string OriginallyCalledDev()
        {
            return Part(14);
        }

        /**
         * 15 - Last_Redirection_Ext
         * 
         * Identifies the extension number of the last device that redirected 
         * (forwarded, deflected, transferred, or recalled) the call, if 
         * applicable; otherwise, this is blank.
         */
        public string LastRedirectionExt()
        {
            return Part(15);
        }

        /**
         * 16 - Account_Code
         * Indicates the account code the system should use for this call.
         */
        public string AccountCode()
        {
            return Part(16);
        }

        /**
         * 17 - Local_Cnx_State
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
            return Part(17);
        }

        /**
         * 18 - Event_Cause
         * 
         * This parameter provides extra information about the cause of the event. 
         * These cause values are encoded as two-digit decimal numbers.
         * 
         * 22 = New Call
         */
        public int EventCause()
        {
            return IntPart(18);
        }

        /**
         * 19 - ACD/UCD_Group
         *
         * Identifies the ACD or UCD hunt group extension. This field is blank if
         * there is no ACD/UCD hunt group associated with the call.
         * 
         */
        public string ACDUCDGroup()
        {
            return Part(19);
        }

        public new void Process()
        {
            // Set the call in the controller
            SetCall();

            // Bind the call to the appropriate devices/agents
            if (0 == OAICallingDeviceType.EXTERNAL.CompareTo(AlertingDeviceType()))
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
        }

        protected void SetCall()
        {
            string call = CallID();

            bool newCall = false;

            if (null !=call &&
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

                CallModel.Trunk = TrunkName();
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
            OAIDebuggerQueue.Relay().Line = "InboundCall";

            AddCallToExtension(AlertingInternalExt());
            CallModel.CLI = OutsideCallerNumber();
        }

        // Internal ----> External
        protected void OutboundCall()
        {
            OAIDebuggerQueue.Relay().Line = "OutboundCall";

            AddCallToExtension(InternalCallingExt());
            CallModel.CLI = AlertingOutsideNumber();
        }

        protected void InternalCall()
        {
            OAIDebuggerQueue.Relay().Line = "InternalCall";

            AddCallToExtension(InternalCallingExt());
            AddCallToExtension(AlertingInternalExt());
        }

        protected void ExternalCall()
        {
            OAIDebuggerQueue.Relay().Line = "ExternalCall";
            // TODO: ???
        }
    }
}
