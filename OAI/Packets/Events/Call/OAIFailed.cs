using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Controllers;

namespace OAI.Packets.Events.Call
{
    /**
     * Failed – FA
     * 
     * Occurs whenever a call cannot be completed. Examples include dialing 
     * an invalid extension, dialing a restricted outside number, dialing a 
     * busy device that does not allow camp ons, dialing a device in DND, 
     * attempting to forward to an invalid destination, etc.
     * 
     * FA,<Resync_Code>,<Mon_Cross_Ref_ID>,<Call_ID>,<Failed_Ext>,
     * <Called_Number>,<Local_Cnx_State>,<Event_Cause>
     */
    public class OAIFailed : OAICall
    {
        public const string EVENT = "FA";

        public OAIFailed(string[] parts) : base(parts) { }
        public OAIFailed(byte[] bytes) : base(bytes) { }

        /**
         * 5 - Failed_Ext
         * 
         * Identifies the station that caused the call to fail (e.g., the busy station, 
         * the tollrestricted station, etc.).
         */
        public string FailedExt()
        {
            return Part(5);
        }

        /**
         * 6 - Called_Number
         * Indicates the intended destination of the call. For an outgoing CO call,
         * this is the outside number.
         */
        public string CalledNumber()
        {
            return Part(6);
        }

        /**
         * 7 - Local_Cnx_State
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
            return Part(7);
        }

        /**
         * 8 - Event_Cause
         * 
         * This parameter provides extra information about the cause of the event. 
         * These cause values are encoded as two-digit decimal numbers.
         */
        public int EventCause()
        {
            return IntPart(8);
        }

        public new void Process()
        {
            // Remove the call from the terminating extension
            DisconnectCallFromExtension(FailedExt());

            string call = CallID();

            if (null != call && 0 < call.Length)
            {
                // Remove the call from the controller
                OAICallsController.
                    Relay().
                    Pop(call);
            }
        }
    }
}
