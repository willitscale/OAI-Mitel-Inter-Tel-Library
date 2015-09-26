using OAI.Structures.Queries;

namespace OAI.Bus
{
    public class OAIAgentBus : OAIBus<OAIQueryExtendedAgent>
    {
        private static OAIAgentBus Instance;

        public static OAIAgentBus Relay()
        {
            if (null == Instance)
            {
                Instance = new OAIAgentBus();
            }

            return Instance;
        }
    }
}
