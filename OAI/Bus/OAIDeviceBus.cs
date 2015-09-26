using OAI.Structures.Queries;

namespace OAI.Bus
{
    public class OAIDeviceBus : OAIBus<OAIQueryExtendedStation>
    {
        private static OAIDeviceBus Instance;

        public static OAIDeviceBus Relay()
        {
            if (null == Instance)
            {
                Instance = new OAIDeviceBus();
            }

            return Instance;
        }
    }
}
