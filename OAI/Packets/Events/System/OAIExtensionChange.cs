using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.System
{
    /**
     * Extension Change – EC 
     * 
     * Occurs whenever device extensions are changed in the database, 
     * or when a device is equipped or unequipped.
     * 
     * EC,<Resync_Code>,<Mon_Cross_Ref_ID>,<Network_Node_Number>,
     * <Old_Extension>,<New_Extension>,<Device_Type>
     */
    public class OAIExtensionChange : OAISystem
    {
        public const string EVENT = "EC";

        public OAIExtensionChange(string[] parts) : base(parts) { }
        public OAIExtensionChange(byte[] bytes) : base(bytes) { }

        /**
         * 4 - Network_Node_Number
         * 
         * Indicates the node number of the connected communications system. 
         * If the system is not networked, this will be “1.” When this event 
         * is sent to an application by the CT Gateway, this field specifies 
         * the node number of the communications system that originally 
         * generated the event.
         */
        public string NetworkNodeNumber()
        {
            return Part(4);
        }

        /**
         * 5 - Old_Extension
         * 
         * Displays the old extension number of the device. If blank, the 
         * device is new.
         */
        public string OldExtension()
        {
            return Part(5);
        }

        /**
         * 6 - New_Extension
         * 
         * Shows the new extension of the device. If blank, the device has 
         * been removed.
         */
        public string NewExtension()
        {
            return Part(6);
        }

        /**
         * 7 - Device_Type
         * 
         * Identifies the device type of the <Old_Extension> and/or <New_Extension>.
         * The values are listed in Table 19, Device_Type Values, on page 70. 
         * Note the device types that are not reported for this event.
         */
        public string DeviceType()
        {
            return Part(7);
        }

        public new void Process()
        {
            // TODO
        }
    }
}
