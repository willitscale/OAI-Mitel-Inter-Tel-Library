using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Call
{
    /**
     * Held – HE
     * 
     * Occurs when a call that was established on a device is now on hold at 
     * that device or another device (transfer-to-hold).
     */
    public class OAIHeld : OAICall
    {
        public const string EVENT = "HE";

        public OAIHeld(string[] parts) : base(parts) { }
        public OAIHeld(byte[] bytes) : base(bytes) { }

        /**
         * 5 - Activate_Hold_Ext
         * 
         * Identifies the station that placed the call on hold.
         */
        public string ActivateHoldExt()
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

        public new void Process()
        {
            // The call is no longer active with the device or agent
            SetActiveCall(ActivateHoldExt(), null);
        }
    }
}
