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
     * Call Cleared – CC
     * 
     * Occurs when a call that was on a device is now terminating and clearing out of the
     * communications system. This event is sent only for calls that are being monitored. If the devices
     * involved in the call are being monitored but the call is not, Connection Cleared (XC) events (see
     * page 45) are the only events sent. The communications system also generates Call Cleared
     * events for every call in the system, on a per-system OAI serial port basis, if a specific monitor
     * type is specified in a Monitor Start (_MS) command (see page 130). This allows the application
     * to receive Call Cleared events for every call in the system without having to initiate a call
     * monitor on each and every call. 
     * 
     * CC,<Resync_Code>,<Mon_Cross_Ref_ID>,<Call_ID>,<Event_Cause>,
     * <Transferred_Call_ID>,<Transfer_Destination>
     */
    public class OAICallCleared : OAICall
    {
        public const string EVENT = "CC";

        public OAICallCleared(string[] parts) : base(parts) { }
        public OAICallCleared(byte[] bytes) : base(bytes) { }

        /**
         * 5 - <Event_Cause>
         */
        public int EventCause()
        {
            return IntPart(5);
        }

        /**
         * 6 - <Transferred_Call_ID>
         * 
         * If <Event_Cause> is 32 (Transfer), specifies the call ID 
         * of the call that was transferred. If <Event_Cause> is any 
         * value other than 32, this field is blank.
         */
        public string TransferredCallID()
        {
            return Part(6);
        }

        /**
         * 7 - <Transfer_Destination>
         * 
         * If <Event_Cause> is 32 (Transfer), specifies the destination 
         * extension. If <Event_Cause> is any value other than 32, this 
         * field is blank.
         */
        public string TransferDestination()
        {
            return Part(7);
        }

        public new void Process()
        {
            if (OAIEventCause.TRANSFER != EventCause())
            {
                return;
            }

            AddCallToExtension(TransferDestination());
        }

    }
}
