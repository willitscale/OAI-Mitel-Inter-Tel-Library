using OAI.Structures.Queries;

namespace OAI.Bus
{
    public class OAIHuntGroupBus : OAIBus<OAIQueryExtendedHuntGroup>
    {
        private static OAIHuntGroupBus Instance;

        public static OAIHuntGroupBus Relay()
        {
            if (null == Instance)
            {
                Instance = new OAIHuntGroupBus();
            }

            return Instance;
        }
    }
}
