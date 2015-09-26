using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Structures.Queries
{
    class OAICallQuery : OAIQuery
    {
        /**
         * <Call_ID>
         */
        public string Call_ID;

        public override string ID()
        {
            return Call_ID;
        }

        /**
         * <Affected_Ext>
         */
        public string Affected_Ext;
    }
}
