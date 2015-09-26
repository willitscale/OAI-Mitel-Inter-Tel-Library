using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Commands
{
    /**
     * Resync Request – _RR
     * 
     * Resynchronizes the communications system and the application. This should be sent whenever
     * the application suspects that it may have missed some events and is out of synchronization with
     * the communications system. In response, the communications system will send the Resync
     * Request confirmation, followed by numerous other events, to provide a complete refresh of the
     * status of all stations monitored (or one specific monitor if <Affected_Ext> is not blank) by the
     * application. After all of the resync-generated events are issued, the communications system
     * sends a Resync Ended (RD) event (see page 84) to indicate that the resync has completed.
     * 
     * After initiating a system resync of all device monitors, applications should avoid sending any
     * further commands until receiving the Resync Ended event. Depending on the number of
     * monitors, it could take the communications system several minutes to send all of the events that
     * resulted from the resync. In addition, the communications system does not send any events
     * until the Resync Ended event is generated. Applications, therefore, may not receive some
     * events until several minutes after the event occurred. 
     */
    class OAIResyncRequest : OAICommand
    {
        public const string CMD = "_RR";

        public OAIResyncRequest() : this( "" ){}

        public OAIResyncRequest(string extension)
        {
            Command = CMD;

            Arguments = new string[1];

            Arguments[0] = extension;
        }

        public override bool Delayed()
        {
            return false;
        }

        public override void Block() { }

        public override void Release() { }
    }
}
