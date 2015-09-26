namespace OAI.Packets.Delays
{
    public class OAICallDelay : OAIDelay
    {
        private static OAICallDelay Instance;

        public static OAICallDelay Relay()
        {
            if (null == Instance)
            {
                Instance = new OAICallDelay();
            }

            return Instance;
        }
    }
}
