using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Structures.Queries
{
    public class OAIQueryExtendedDND : OAIQuery
    {
        public override string ID()
        {
            return DND_Message_Number;
        }

        /**
         * 10 DND Message List
         */

        /**
         * <DND_Message_Number> Number (1-20) assigned to the DND message.
         */
        public string DND_Message_Number;

        /**
         * <|DND_Message_Text|> Text assigned to the DND message number.
         */
        public string DND_Message_Text;
    }
}
