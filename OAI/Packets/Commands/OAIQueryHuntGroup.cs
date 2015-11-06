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

        protected bool Master;

        public OAIQueryHuntGroup(string huntGroup)
            : this(huntGroup, false) { }

        public OAIQueryHuntGroup(string huntGroup, bool master)
        {
            Command = CMD;

            // _QH,<InvokeID>,<Affected_Ext><CR>
            Arguments = new string[1];

            // Affected_Ext
            // Where the valid device type for <Affected_Ext> is an ACD or 
            // UCD hunt group pilot number.
            Arguments[0] = huntGroup.ToString();

            Master = master;
        }

        public override bool Delayed()
        {
            return false;
        }

        public override void Block() { }

        public override void Release() { }

        public string GetHuntGroup()
        {
            return Arguments[0];
        }

        public bool GetMaster()
        {
            return Master;
        }
    }
}
