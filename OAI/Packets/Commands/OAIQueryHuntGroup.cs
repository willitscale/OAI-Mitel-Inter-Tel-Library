using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Commands
{
    /**
     * Query Hunt Group – _QH
     * 
     * Provides the current mode of every ACD/UCD agent who is a member 
     * of the specified ACD/UCD hunt group.
     */
    public class OAIQueryHuntGroup : OAICommand
    {
        public const string CMD = "_QH";

        public OAIQueryHuntGroup(int huntGroup)
        {
            Command = CMD;

            // _QH,<InvokeID>,<Affected_Ext><CR>
            Arguments = new string[1];

            // Affected_Ext
            // Where the valid device type for <Affected_Ext> is an ACD or 
            // UCD hunt group pilot number.
            Arguments[0] = huntGroup.ToString();
        }

        public override bool Delayed()
        {
            return false;
        }

        public override void Block() { }

        public override void Release() { }
    }
}
