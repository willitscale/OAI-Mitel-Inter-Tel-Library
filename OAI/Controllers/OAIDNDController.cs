using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Models;

namespace OAI.Controllers
{
    public class OAIDNDController : OAIController<OAIDNDModel>
    {
        public static OAIDNDController Instance;

        public static OAIDNDController Relay()
        {
            if (null == Instance)
            {
                Instance = new OAIDNDController();
            }

            return Instance;
        }
    }
}
