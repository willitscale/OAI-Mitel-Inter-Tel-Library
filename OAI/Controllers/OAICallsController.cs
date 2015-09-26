using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Models;

namespace OAI.Controllers
{
    public class OAICallsController : OAIController<OAICallModel>
    {
        public static OAICallsController Instance;

        public static OAICallsController Relay()
        {
            if (null == Instance)
            {
                Instance = new OAICallsController();
            }

            return Instance;
        }
    }
}
