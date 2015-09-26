namespace OAI.Packets.Delays
{
    public class OAIDeviceDelay : OAIDelay
    {
        private static OAIDeviceDelay Instance;

        public static OAIDeviceDelay Relay()
        {
            if (null == Instance)
            {
                Instance = new OAIDeviceDelay();
            }

            return Instance;
        }
    }
}
