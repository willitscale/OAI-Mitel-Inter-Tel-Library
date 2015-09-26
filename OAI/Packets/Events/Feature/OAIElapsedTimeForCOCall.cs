using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Feature
{
    /**
     * Elapsed Time For CO Call – ET
     * 
     * Occurs when a station connects to a CO call.
     * 
     * ET,<Resync_Code>,<Mon_Cross_Ref_ID>,<Call_ID>,<Affected_Ext>,
     * <Number_of_Elapsed_Seconds><CR><LF>
     */
    public class OAIElapsedTimeForCOCall : OAIFeature
    {
        public const string EVENT = "ET";

        public OAIElapsedTimeForCOCall(string[] parts) : base(parts) { }
        public OAIElapsedTimeForCOCall(byte[] bytes) : base(bytes) { }

        /**
         * 4 - Call_ID
         * 
         * Identifies the connected call.
         */
        public string CallID()
        {
            return Part(4);
        }

        /**
         * 5 - Affected_Ext
         * 
         * Indicates the station connected to the CO call.
         */
        public string AffectedExt()
        {
            return Part(5);
        }

        /**
         * 6 - Number_of_Elapsed_Seconds
         * 
         * Indicates the total number of seconds that this call has been 
         * in the system.
         */
        public int NumberOfElapsedSeconds()
        {
            return IntPart(6);
        }

        public new void Process()
        {
            // TODO
        }
    }
}
