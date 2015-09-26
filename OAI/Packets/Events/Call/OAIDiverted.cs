using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Call
{
    /**
     * Diverted – DI
     * 
     * Occurs when the system delivers or attempts to deliver a call to a device 
     * that redirects the call to another device. The call may be redirected 
     * immediately (e.g., immediate forward) or after it has already rung at 
     * the redirecting device (e.g., delayed manual forward, transfer recall, etc.).
     */
    public class OAIDiverted : OAICall
    {
        public const string EVENT = "DI";

        public OAIDiverted(string[] parts) : base(parts) { }
        public OAIDiverted(byte[] bytes) : base(bytes) { }

        /**
         * 5 - <Diverted_From_Ext>
         * 
         * Identifies the extension number of the station ringing before the call
         * was diverted.
         */
        public string DivertedFromExt()
        {
            return Part(5);
        }

        /**
         * 6 - <New_Dest_Ext>
         * 
         * Indicates the extension number of the call’s new destination (i.e., where
         * the call rings after being diverted). If the call is diverted to a public 
         * network, this is the extension number of the trunk.
         */
        public string NewDestExt()
        {
            return Part(6);
        }

        /**
         * 7 - <Diverted_To_Outside_Number>
         * 
         * Indicates the outside number if the call was diverted to the
         * public network. 
         */
        public string DivertedToOutsideNumber()
        {
            return Part(7);
        }

        /**
         * 8 - <Local_Cnx_State>
         * ...
         */
        public string LocalCnxState()
        {
            return Part(8);
        }

        /**
         * 9 - <Event_Cause>
         * ...
         */
        public int EventCause()
        {
            return IntPart(9);
        }

        public new void Process()
        {
            // Remove the call from the diverted extension
            DisconnectCallFromExtension(DivertedFromExt());

            // Add the call from the diverting extension
            AddCallToExtension(NewDestExt());
        }
    }
}
