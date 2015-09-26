using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Structures.Queries
{
    public class OAIQQueryHuntGroup : OAIQuery
    {
        public override string ID()
        {
            return Agent_ID;
        }

        /**
         * 6 <Agent_ID>
         * Identifies the ACD agent. 
         */
        public string Agent_ID;

        /**
         * 7 <Device_Ext>
         *  Indicates the station associated with the ACD agent 
         *  represented by <Agent_ID>. This is the extension of 
         *  the station where <Agent_ID> logged into the hunt 
         *  group if the agent is logged in. If the agent is 
         *  currently logged out of the hunt group, this field 
         *  is blank.
         */
        public string Device_Ext;

        /**
         * 8 <ACD/UCD_Mode>
         * Indicates the Agent ID’s current mode in the ACD or 
         * UCD Hunt Group. The options for the <ACD_Mode> field are 
         * as follows:
         *      0 = Not Logged In
         *      1 = Logged In and Ready
         *      2 = Logged In and Not Ready
         *      3 = Logged In and Work Not Ready
         * The options for the <UCD_Mode> field are:
         *      4 = UCD Ready
         *      5 = UCD Busy
         */
        public int ACD_UCD_Mode;

        public bool Available()
        {
            return (1 == ACD_UCD_Mode);
        }

        public string Status()
        {
            switch(ACD_UCD_Mode)
            {
                case 1 :
                {
                    return "Logged In and Ready";
                }
                case 2:
                {
                    return "Logged In and Not Ready";
                }
                case 3:
                {
                    return "Logged In and Work Not Ready";
                }
                case 4:
                {
                    return "UCD Ready";
                }
                case 5:
                {
                    return "UCD Busy";
                }
            }

            return "Not Logged In";
        }
    }
}
