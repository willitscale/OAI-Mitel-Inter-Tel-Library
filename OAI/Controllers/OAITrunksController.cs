using OAI.Models;

namespace OAI.Controllers
{
    public class OAITrunksController : OAIController<OAITrunkModel>
    {
        public static OAITrunksController Instance;

        public static OAITrunksController Relay()
        {
            if (null == Instance)
            {
                Instance = new OAITrunksController();
            }

            return Instance;
        }
    }
}
