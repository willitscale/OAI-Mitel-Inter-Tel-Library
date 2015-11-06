using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Models;

namespace OAI.Controllers
{
    public class OAIFeatureController : OAIController<OAIFeatureModel>
    {
        public static OAIFeatureController Instance;

        public static OAIFeatureController Relay()
        {
            if (null == Instance)
            {
                Instance = new OAIFeatureController();
            }

            return Instance;
        }
    }
}
