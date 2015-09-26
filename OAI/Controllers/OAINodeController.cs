using OAI.Models;

namespace OAI.Controllers
{
    public class OAINodeController : OAIController<OAINodeModel>
    {
        public static OAINodeController Instance;

        public static OAINodeController Relay()
        {
            if (null == Instance)
            {
                Instance = new OAINodeController();
            }

            return Instance;
        }
    }
}
