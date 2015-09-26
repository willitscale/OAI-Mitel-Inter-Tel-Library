using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Models;

namespace OAI.Controllers
{
    public class OAIDevicesController : OAIController<OAIDeviceModel>
    {
        public static OAIDevicesController Instance;

        public static OAIDevicesController Relay()
        {
            if (null == Instance)
            {
                Instance = new OAIDevicesController();
            }

            return Instance;
        }
    }
}
