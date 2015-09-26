using OAI.Structures.Queries;

namespace OAI.Bus
{
    public class OAIDNDBus : OAIBus<OAIQueryExtendedDND>
    {
        private static OAIDNDBus Instance;

        public static OAIDNDBus Relay()
        {
            if (null == Instance)
            {
                Instance = new OAIDNDBus();
            }

            return Instance;
        }
    }
}
