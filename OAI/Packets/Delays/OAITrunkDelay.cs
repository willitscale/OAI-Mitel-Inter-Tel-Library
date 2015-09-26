namespace OAI.Packets.Delays
{
    public class OAITrunkDelay : OAIDelay
    {
        private static OAITrunkDelay Instance;

        public static OAITrunkDelay Relay()
        {
            if (null == Instance)
            {
                Instance = new OAITrunkDelay();
            }

            return Instance;
        }
    }
}
