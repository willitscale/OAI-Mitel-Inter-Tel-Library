namespace OAI.Packets.Commands
{
    /**
     * No Operation – _NO
     * 
     * Allows the attached computer to verify that the System OAI link and 
     * system are operational. The communications system simply replies 
     * back without performing any operation.
     */
    class OAINoOperation : OAICommand
    {
        public const string CMD = "_NO";

        public OAINoOperation()
        {
            Command = CMD;

            Arguments = new string[0];
        }

        public override bool Delayed()
        {
            return false;
        }

        public override void Block() { }

        public override void Release() { }
    }
}
