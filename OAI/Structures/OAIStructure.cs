using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Structures
{
    /**
     * OAI Structures
     * 
     *  Once we set the data for a data structure we should treat it as
     *  essentially immutable.
     */
    public abstract class OAIStructure
    {
        public abstract string ID();
    }
}
