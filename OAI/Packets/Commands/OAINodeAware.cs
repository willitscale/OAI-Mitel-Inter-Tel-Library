using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Commands
{
    /**
     * Node Aware – _NA
     * 
     * Sent by the application to inform the CT Gateway that it wants 
     * to operate in a node aware state. The node aware state also changes 
     * the way the CT Gateway returns the No Operation (_NO) confirmation 
     * (see page 140) and the Link Status (LS) event (see page 91).
     */
    class OAINodeAware : OAICommand
    {
        public OAINodeAware() : this( true ) {}

        public OAINodeAware(bool aware)
        {
            Command = "_NA";
            Arguments = new string[1];
            Arguments[0] = (aware) ? "1" : "0";
        }

        public override bool Delayed()
        {
            return false;
        }

        public override void Block() { }

        public override void Release() { }
    }
}
