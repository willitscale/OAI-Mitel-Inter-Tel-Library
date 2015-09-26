using OAI.Structures.Queries;

namespace OAI.Bus
{
    public class OAIAgentHuntGroupBus : OAIBus<OAIQQueryHuntGroup>
    {
        private static OAIAgentHuntGroupBus Instance;

        public static OAIAgentHuntGroupBus Relay()
        {
            if (null == Instance)
            {
                Instance = new OAIAgentHuntGroupBus();
            }

            return Instance;
        }
    }
}
