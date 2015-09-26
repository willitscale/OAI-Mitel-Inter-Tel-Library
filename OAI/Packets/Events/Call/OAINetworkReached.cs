using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Models;
using OAI.Controllers;

namespace OAI.Packets.Events.Call
{
    /**
     * Network Reached – NT
     * 
     * Occurs when a call reaches a trunk, indicating that further call progress 
     * events may not be possible for this call. Once a call reaches an outgoing 
     * trunk, the communications system sends a Network Reached event which may 
     * or may not be followed by Delivered (see page 46) and Established (see page 
     * 48) events.
     * 
     * NT,<Resync_Code>,<Mon_Cross_Ref_ID>,<Call_ID>,<Trunk_Used>,
     * <Dialed_Number>,<Local_Cnx_State>,<Event_Cause>
     */
    public class OAINetworkReached : OAICall
    {
        public const string EVENT = "NT";

        protected OAICallModel CallModel;

        public OAINetworkReached(string[] parts) : base(parts) { }
        public OAINetworkReached(byte[] bytes) : base(bytes) { }

        /**
         * 5 - <Trunk_Used>
         * 
         * Identifies the extension number of the trunk receiving the call.
         */
        public string TrunkUsed()
        {
            return Part(5);
        }

        /**
         * 6 - <Dialed_Number>
         * 
         * Indicates the outside number dialed, if known. A Network Reached event
         * sent on behalf of a station device will complete this field only when a 
         * call is forwarded or transferred to the public network. When this event 
         * gets sent on behalf of the network device (trunk) or the call itself, 
         * this field is complete for these cases (i.e., when a call is forwarded or 
         * transferred to the public network) and for when the call originates through
         * ARS. If the Make Call (_MC) command (see page 125) is used to make the call, 
         * this field is complete for all parties. 
         */
        public string DialedNumber()
        {
            return Part(6);
        }

        /**
         * 7 - <Local_Cnx_State>
         * ...
         */
        public string LocalCnxState()
        {
            return Part(7);
        }

        /**
         * 8 - <Event_Cause>
         * ...
         */
        public string EventCause()
        {
            return Part(8);
        }

        public new void Process()
        {
            SetCall();
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

                CallModel.Call = call;
                CallModel.CLI = DialedNumber();
                CallModel.Trunk = TrunkUsed();

                if (newCall)
                {
                    OAICallsController.Relay().Push(call, CallModel);
                }
            }
        }
    }
}
