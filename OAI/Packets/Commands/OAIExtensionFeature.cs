using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Packets.Delays;

namespace OAI.Packets.Commands
{
    /**
     * Extension Feature – _EF
     * 
     * Allows the application to simulate an extension or feature being 
     * accessed on a station device. This command also allows the application 
     * to dial digits over an existing call by specifying the Call ID in the 
     * <Call_ID> field. If the <Affected_Ext> is busy and a Call ID is not 
     * specified, this command will fail. Because the station device enters 
     * the digit string exactly as specified, this command may leave the 
     * device in a non-idle state. This, however, should not be an issue
     * because the station should always time out and eventually return to 
     * idle. Because there is no way to force a single line off-hook, 
     * this command fails if sent to an on-hook single line.
     */
    public class OAIExtensionFeature : OAICommand
    {
        public const string CMD = "_EF";

        public OAIExtensionFeature(string extension, string call, int feature)
        {
            // _EF,<InvokeID>,<Affected_Ext>,<Call_ID>,<Extension/Feature_String>
            Arguments = new string[3];

            // Affected_Ext: Indicates a phone (online)
            Arguments[0] = extension;

            Arguments[1] = ( null == call ) ? "" : call;

            // Extension/Feature_String: Includes the digit string for the 
            // extension number or feature code to be dialed. Depending on how 
            // the communications system is programmed, the feature code may 
            // require an exclamation point (!) at the beginning of the digit string, 
            // even when the station is idle (see Appendix A, page 223, for 
            // a list of default feature codes). If the station is in the 
            // connected state and the <Call_ID> field is blank, only those feature
            // codes preceded by an exclamation point will be accepted. This 
            // field may be up to 48 digits. See Table 35 (below) for possible values.
            Arguments[2] = feature.ToString();
        }

        public string Extension()
        {
            return Arguments[0];
        }

        public string Call()
        {
            return Arguments[2];
        }

        public override bool Delayed()
        {
            return OAIDeviceDelay.Relay().Delayed(Extension()) ||
                OAICallDelay.Relay().Delayed(Call());
        }

        public override void Block()
        {
            OAIDeviceDelay.Relay()
                .Block(Extension());

            OAICallDelay.Relay()
                .Block(Call());
        }

        public override void Release()
        {
            OAIDeviceDelay.Relay()
                .Release(Extension());

            OAICallDelay.Relay()
                .Release(Call());
        }
    }
}
