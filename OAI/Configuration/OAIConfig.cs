namespace OAI.Configuration
{
    public class OAIConfig
    {
        /**
         * Station Message Detail Recording (SMDR) Access
         */
        public const byte SMDR_ACCESS = 0x84;

        /**
         * System OAI Login Message
         */
        public const byte SOAI_LOGIN = 0x87;

        public OAIConfig(string host, int port, string name ) 
            : this( host, port, name, "", SOAI_LOGIN) { }

        public OAIConfig(string host, int port, string name, 
            string password)
            : this(host, port, name, password, SOAI_LOGIN) { }

        public OAIConfig(string host, int port, string name, 
            string password, byte type)
        {
            Host = host;
            Port = port;
            Name = name;
            Password = password;
            Type = type;
        }

        public string Host { get; set; }
        public int Port { get; set; }
        public byte Type { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
