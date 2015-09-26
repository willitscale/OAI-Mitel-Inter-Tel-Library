using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Misc
{
    /**
     * Monitor Ended – ME
     * 
     * Occurs when the system can no longer provide the requested 
     * events associated with the specified <Mon_Cross_Ref_ID>. 
     * This event is unsolicited.
     * 
     * ME,<Resync_Code>,<Mon_Cross_Ref_ID>,<Event_Cause>
     */
    public class OAIMonitorEnded : OAIMisc
    {
        public const string EVENT = "ME";

        public OAIMonitorEnded(string[] parts) : base(parts) { }
        public OAIMonitorEnded(byte[] bytes) : base(bytes) { }

        /**
         * 4 - Event_Cause
         * 
         */
        public string EventCause()
        {
            return Part(4);
        }

        public new void Process()
        {
            // TODO
        }
    }
}
