using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Models;

namespace OAI.Controllers
{
    public class OAIHuntGroupsController : OAIController<OAIHuntGroupModel>
    {
        public static OAIHuntGroupsController Instance;

        public static OAIHuntGroupsController Relay()
        {
            if (null == Instance)
            {
                Instance = new OAIHuntGroupsController();
            }

            return Instance;
        }
    }
}
