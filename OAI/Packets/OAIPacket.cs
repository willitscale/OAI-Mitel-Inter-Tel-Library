namespace OAI.Packets
{
    public abstract class OAIPacket
    {
        public abstract bool Delayed();

        public abstract void Block();

        public abstract void Release();
    }
}
