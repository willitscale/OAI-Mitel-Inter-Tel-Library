using OAI.Structures.Queries;

namespace OAI.Bus
{
    class OAICallBus : OAIBus<OAICallQuery>
    {
        private static OAICallBus Instance;

        public static OAICallBus Relay()
        {
            if (null == Instance)
            {
                Instance = new OAICallBus();
            }

            return Instance;
        }
    }
}
