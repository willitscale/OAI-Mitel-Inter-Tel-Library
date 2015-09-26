using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Feature
{
    /**
     * Feature Status – FE
     * 
     * Occurs when a phone enables or disables a toggleable feature
     * 
     * When <Resync_Code> is not empty:
     * FE,<Resync_Code>,<Mon_Cross_Ref_ID>,<Extension>,<Feature_Status_Mask><CR><LF>
     * 
     * When <Resync_Code> is empty:
     * FE,<Resync_Code>,<Mon_Cross_Ref_ID>,<Extension>,<Logical_Number>,<On/Off><CR><LF>
     */
    public class OAIFeatureStatus : OAIFeature
    {
        public const string EVENT = "FE";

        public OAIFeatureStatus(string[] parts) : base(parts) { }
        public OAIFeatureStatus(byte[] bytes) : base(bytes) { }

        /**
         * 4 - Extension
         * 
         * Identifies the extension number of the station device.
         */
        public string Extension()
        {
            return Part(4);
        }

        /**
         * 5 - Feature_Status_Mask
         * 
         * Specifies a bit mask where each bit in the hexadecimal number indicates 
         * whether a feature is enabled (1) or disabled (0). See Table 16 for a list 
         * of feature codes and their associated bit numbers. This parameter is used 
         * only for resync-generated events (i.e., <Resync_Code> is not empty).
         */
        public string FeatureStatusMask()
        {
            return Part(5);
        }

        /**
         * 5 - Logical_Number
         * 
         * Identifies the feature logical number (see the following table). This
         * parameter exists only when <Resync_Code> is empty.
         */
        public string LogicalNumber()
        {
            return Part(5);
        }

        /**
         * 6 - On/Off
         * 
         * Indicates whether the feature has been enabled or disabled (1 = Enabled; 
         * 0 = Disabled). This parameter exists only when <Resync_Code> is empty.
         */
        public int OnOff()
        {
            return IntPart(6);
        }

        public new void Process()
        {
            // TODO
        }
    }
}
