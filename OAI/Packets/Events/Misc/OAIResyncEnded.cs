using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Misc
{
    /**
     * Resync Ended – RD
     * 
     * Sent when a resync is completed, including resync requests, 
     * a monitor start on all stations with a resync request, a monitor 
     * start on a single device with a resync request, and power-up 
     * resyncs. This does not include the Resync Time (RT) command.
     * 
     * RD,<Resync_Code>,<Specific_Extension>
     */
    public class OAIResyncEnded : OAIMisc
    {
        public const string EVENT = "RD";

        public OAIResyncEnded(string[] parts) : base(parts) { }
        public OAIResyncEnded(byte[] bytes) : base(bytes) { }

        /**
         * 3 - Specific_Extension
         * 
         * 
         * Where <Specific_Extension> identifies the device that has just 
         * completed a resync request. If blank, the resync request was a 
         * global system resync.
         */
        public string SpecificExtension()
        {
            return Part(3);
        }

        public new void Process()
        {
            // TODO 
        }
    }
}
