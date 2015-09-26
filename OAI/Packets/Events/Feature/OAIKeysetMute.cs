using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Feature
{
    /**
     * Keyset Mute – MU
     * 
     * Occurs whenever a phone enables or disables the mute feature.
     * 
     * MU,<Resync_Code>,<Mon_Cross_Ref_ID>,<Extension>,<On/Off>
     */
    public class OAIKeysetMute : OAIFeature
    {
        public const string EVENT = "MU";

        public OAIKeysetMute(string[] parts) : base(parts) { }
        public OAIKeysetMute(byte[] bytes) : base(bytes) { }

        /**
         * 4 - Extension
         * 
         * Identifies the extension number of the device.
         */
        public string Extension()
        {
            return Part(4);
        }

        /**
         * 5 - On/Off
         * 
         * Indicates whether the mute mode has been disabled or enabled. 
         * This is a boolean parameter (1 = Enabled; 0 = Disabled).
         */
        public string OnOff()
        {
            return Part(5);
        }

        public new void Process()
        {
            // TODO
        }
    }
}
