using OAI.Structures.Queries;

namespace OAI.Bus
{
    public class OAIFeatureBus : OAIBus<OAIQueryExtendedFeature>
    {
        private static OAIFeatureBus Instance;

        public static OAIFeatureBus Relay()
        {
            if (null == Instance)
            {
                Instance = new OAIFeatureBus();
            }

            return Instance;
        }
    }
}
