using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Structures.Queries
{
    public class OAIQueryExtendedAgent : OAIQuery
    {
        /**
         * ACD Agent List
         * 
         * <|Description|> Description assigned to the agent.
         */

        /**
         * <Agent_ID> The ID of the agent.
         */
        public string Agent_ID;

        public override string ID()
        {
            return Agent_ID;
        }
    }
}
