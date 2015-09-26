namespace OAI.Packets.Delays
{
    public class OAINodeDelay : OAIDelay
    {
        private static OAINodeDelay Instance;

        public static OAINodeDelay Relay()
        {
            if (null == Instance)
            {
                Instance = new OAINodeDelay();
            }

            return Instance;
        }
    }
}
