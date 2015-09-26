using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Structures.Queries
{
    public class OAIQueryExtendedHuntGroup : OAIQuery
    {
        public override string ID()
        {
            if (null == Extension)
            {
                return null;
            }

            return Extension;
        }

        /**
         * Hunt Group List
         * 
         * <Extension> Extension of the hunt group.
         * <|Username|> Username assigned to the hunt group.
         * <|Description|> Description assigned to the hunt group.
         */

        /**
         * <Hunt_Group_Type> 
         *      0 = UCD Hunt Group
         *      1 = ACD Hunt Group without ACD Agent IDs
         *      2 = ACD Hunt Group with ACD Agent IDs
         *      3 = Personal Router Device Hunt Group
         */
        public int Hunt_Group_Type;

        /** 
         * <Number_Of_Members> Total number of members or agent IDs for the Hunt Group.
         */
        public int Number_Of_Members;

        /** 
         * <Mailbox_Node_Number> The node containing the hunt group’s associated mailbox. If
         *      zero, the hunt group does not have an associated mailbox on
         *      any node. Extension lists count as one member, regardless of how
         *      many extensions are in the list. Each time an extension occurs in 
         *      the hunt path, it counts as a member. For example, if the hunt group 
         *      has members of 1000, 1001, and 1000 again, it is counted as 3 members. 
         */
        public int Mailbox_Node_Number;
    }
}
