using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Models;

namespace OAI.Controllers
{
    public class OAIAgentsController : OAIController<OAIAgentModel>
    {
        public static OAIAgentsController Instance;

        public static OAIAgentsController Relay()
        {
            if (null == Instance)
            {
                Instance = new OAIAgentsController();
            }

            return Instance;
        }
    }
}
