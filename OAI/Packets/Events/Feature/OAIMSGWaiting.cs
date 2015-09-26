using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Feature
{
    /**
     * MSG Waiting – MW
     * 
     * Occurs when a new message has been left or a message has been 
     * canceled at a station. The <Mailbox> parameter applies only to 
     * voice mail devices leaving/canceling messages. It is possible 
     * to have multiple voice mailboxes use the same device for 
     * notification (i.e., multiple mailboxes can light the message 
     * lamp at the same station). 
     * 
     * MW,<Resync_Code>,<Mon_Cross_Ref_ID>,<Device_Receiving_Message>,
     * <Device_Leaving_Message>,<Mailbox>,<Number_Of_Messages>,<On/Off>
     */
    public class OAIMSGWaiting : OAIFeature
    {
        public const string EVENT = "MW";

        public OAIMSGWaiting(string[] parts) : base(parts) { }
        public OAIMSGWaiting(byte[] bytes) : base(bytes) { }

        /**
         * 4 - Device_Receiving_Message
         * 
         * Identifies the extension number of the device whose message waiting 
         * indication has changed.
         */
        public string DeviceReceivingMessage()
        {
            return Part(4);
        }

        /**
         * 5 - Device_Leaving_Message
         * 
         * Indicates the extension number of the device that left the message.
         */
        public string DeviceLeavingMessage()
        {
            return Part(5);
        }

        /**
         * 6 - Mailbox
         * 
         * Identifies the mailbox where the message was left if voice mail 
         * was used.
         */
        public string Mailbox()
        {
            return Part(6);
        }

        /**
         * 7 - Number_Of_Messages
         * 
         * Indicates the number of messages left by the <Device_Leaving_Message>. 
         * If blank, the Message Waiting indication is disabled.
         */
        public string NumberOfMessages()
        {
            return Part(7);
        }

        /**
         * 8 - On/Off
         * 
         * Indicates the state of the message waiting indication (1 = Enabled; 
         * 0 = Disabled).
         */
        public string OnOff()
        {
            return Part(8);
        }

        public new void Process()
        {
            // TODO
        }
    }
}
