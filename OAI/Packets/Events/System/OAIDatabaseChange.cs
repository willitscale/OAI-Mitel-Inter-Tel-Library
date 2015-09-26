using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.System
{
    /**
     * Database Change – DC
     * 
     * Occurs whenever System OAI-related areas of the database change. The 
     * <Database_Area> parameter specifies the database area where changes 
     * have occurred, as described below.
     * 
     * DC,<Resync_Code>,<Mon_Cross_Ref_ID>,<Network_Node_Number>,
     * <Database_Area>,[Database_Changes]
     */
    public class OAIDatabaseChange : OAISystem
    {
        public const string EVENT = "DC";

        public OAIDatabaseChange(string[] parts) : base(parts) { }
        public OAIDatabaseChange(byte[] bytes) : base(bytes) { }

        /**
         * 4 - Network_Node_Number
         * 
         * Indicates the node number of the connected communications
         * system. If the system is not networked, this will be “1.” 
         * When this event is sent to an application by the CT Gateway, 
         * this field specifies the node number of the communications
         * system that originally generated the event.
         */
        public string NetworkNodeNumber()
        {
            return Part(4);
        }

        /**
         * 5 - Database_Area
         * 
         * Indicates the area of the database that has changed, specifying 
         * the form of the [Database_Changes] field (starting with Table 
         * 20 on page 71).
         */
        public string DatabaseArea()
        {
            return Part(5);
        }

        /**
         * Database_Changes
         * 
         * Includes several fields that are dependent on the <Database_Area>.
         * The first field (<Device_Type>) is the identifier that indicates 
         * which object of the <Database_Area> has changed (e.g., Extension). 
         * The fields following the identifiers describe what has changed for 
         * the specified database object (e.g., Username).
         */
        public void DatabaseChanges()
        {

        }

        public new void Process()
        {

        }
    }
}
