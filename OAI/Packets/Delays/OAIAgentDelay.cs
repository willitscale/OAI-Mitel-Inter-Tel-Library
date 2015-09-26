namespace OAI.Packets.Delays
{
    public class OAIAgentDelay : OAIDelay
    {
        private static OAIAgentDelay Instance;

        public static OAIAgentDelay Relay()
        {
            if (null == Instance)
            {
                Instance = new OAIAgentDelay();
            }

            return Instance;
        }
    }
}
