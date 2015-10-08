using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Controllers;
using OAI.Queues.Changes;

namespace OAI.Packets.Events.Call
{
    /**
     * Connection Cleared – XC
     * 
     * Occurs when a connection that was on a device terminates. 
     * These events are sent any time a monitored device hangs up or 
     * otherwise drops out of a call. The <Call_ID> and call may still be
     * alive in the system at other extension(s), but the connection to 
     * the specified extension (<Cleared_Ext>) is terminated (e.g., one 
     * device drops out of a conference).
     * 
     * XC,<Resync_Code>,<Mon_Cross_Ref_ID>,<Call_ID>,<Cleared_Ext>,
     * <Terminating_Ext>,<Local_Cnx_State>,<Event_Cause>
     */
    public class OAIConnectionCleared : OAICall
    {
        public const string EVENT = "XC";

        public OAIConnectionCleared(string[] parts) : base(parts) { }
        public OAIConnectionCleared(byte[] bytes) : base(bytes) { }

        /**
         * 5 - <Cleared_Ext>
         * Identifies the device that is no longer involved in the call. For CO calls, this in
         * the internal trunk extension used for the call.
         */
        public string ClearedExt()
        {
            return Part(5);
        }

        /**
         * 6 - <Terminating_Ext>
         * Indicates the device that initiated the termination of this connection. For
         * example, in a two-party call, this is the device that went on-hook first. For CO calls, this is
         * the trunk extension used for the call
         */
        public string TerminatingExt()
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
         * 
         * 22 = New Call
         */
        public int EventCause()
        {
            return IntPart(8);
        }

        public new void Process()
        {
            // Remove the call from the cleared extension
            DisconnectCallFromExtension(ClearedExt());

            // Remove the call from the terminating extension
            DisconnectCallFromExtension(TerminatingExt());

            string call = CallID();

            if (null != call && 0 < call.Length)
            {
                // Remove the call from the controller
                OAICallsController.
                    Relay().
                    Pop(call);

                // Notify the change queue that the call has been removed
                OAICallChangeQueue.Relay().Line = call;
            }
        }
    }
}
