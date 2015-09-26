using OAI.Models;

namespace OAI.Packets.Events.Feature
{
    /**
     * Do-Not-Disturb – DND
     * 
     * Occurs when a device enables or disables the do-not-disturb (DND) feature. 
     * When the <DND_Message> field is blank, the device is disabling DND.
     * 
     * DND,<Resync_Code>,<Mon_Cross_Ref_ID>,<DND_Ext>,<DND_Message>,
     * <DND_Text><CR><LF>
     */
    public class OAIDoNotDisturb : OAIFeature
    {
        public const string EVENT = "DND";

        public OAIDoNotDisturb(string[] parts) : base(parts) { }
        public OAIDoNotDisturb(byte[] bytes) : base(bytes) { }

        /**
         * 3 - DND_Ext
         * 
         * Identifies the extension number of the device changing its DND status.
         */
        public string DNDExt()
        {
            return Part(4);
        }

        /**
         * 4 - DND_Message
         * 
         * Displays the text string, up to 16 characters, of the system-programmed
         * DND message.
         */
        public string DNDMessage()
        {
            return Part(5);
        }

        /**
         * 5 - DND_Text
         * 
         * Displays the text string, up to 16 characters, of the user-programmable
         * message
         */
        public string DNDText()
        {
            return Part(6);
        }

        public new void Process()
        {
            string extension = DNDExt();

            if (null != extension &&
                0 != "".CompareTo(extension))
            {
                OAIDeviceModel device = GetDevice(extension);

                if (null != device)
                {
                    device.DNDMessage = DNDMessage();
                    device.DNDText = DNDText();
                }
            }
        }
    }
}
