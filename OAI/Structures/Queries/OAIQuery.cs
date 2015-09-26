using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Structures.Queries
{
    public abstract class OAIQuery : OAIStructure
    {
        public string Extension;
        public string Username;
        public string Description;
    }
}
