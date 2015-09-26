using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Feature
{
    /**
     * Device Offline – OL
     * 
     * Occurs when a station, trunk, hunt group, trunk group, or voice mail application 
     * goes offline or online. This event also occurs with the resync of a station, trunk, 
     * hunt group, trunk group, or voice mail application if the device is offline. The 
     * following table lists some conditions that indicate when a device is offline.
     * 
     * OL,<Resync_Code>,<Mon_Cross_Ref_ID>,<Extension>,<Online/Offline>,
     * <Device_Type>,<Physical_Device_Type>
     */
    public class OAIDeviceOffline : OAIFeature
    {
        public const string EVENT = "OL";

        public OAIDeviceOffline(string[] parts) : base(parts) { }
        public OAIDeviceOffline(byte[] bytes) : base(bytes) { }

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
         * 5 - Online/Offline
         * 
         * Indicates whether the device is currently online (1) or offline (0).
         */
        public int OnlineOffline()
        {
            return IntPart(4);
        }

        /**
         * 6 - Device_Type
         * 
         * Identifies the device type of the <Affected_Ext>, which are listed in Table 19,
         * Device_Type Values, on page 70. Note the device types that are not reported for 
         * this event.
         */
        public string DeviceType()
        {
            return Part(4);
        }

        /**
         * 7 - Physical_Device_Type
         * 
         * Indicates the physical device connected to the communications system. if the 
         * device is offline, this field is 0 (Unknown). After the device goes online, this
         * field changes to reflect the device type. See Table 42 on page 150 for a list 
         * of possible values.
         */
        public string PhysicalDeviceType()
        {
            return Part(4);
        }

        public new void Process()
        {
            // TODO
        }
    }
}
