using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Gateway
{
    /**
     * Gateway Data from Application – GDA
     * 
     * Allows an application to send another application data through the CT Gateway.
     * 
     * GDA,<Resync_Code>,<Monitor_Xref>,<Sending_Application>,
     * <Sending_Application_Name>,<Sending_Application_Address>,<Byte_Count>,
     * <Data_Sent>
     */
    public class OAIGatewayDataFromApplication : OAIGateway
    {
        public const string EVENT = "GDA";

        public OAIGatewayDataFromApplication(string[] parts) : base(parts) { }
        public OAIGatewayDataFromApplication(byte[] bytes) : base(bytes) { }

        /**
         * 3 - Monitor_Xref
         * 
         * Is blank
         */
        public string MonitorXref()
        {
            return Part(3);
        }

        /**
         * 4 - Sending_Application
         * 
         * Indicates the application number of the application sending the data.
         */
        public string SendingApplication()
        {
            return Part(4);
        }

        /**
         * 5 - Sending_Application_Name
         * 
         * Indicates the name of the application sending the data, which
         * is delimited by || characters.
         */
        public string SendingApplicationName()
        {
            return Part(5);
        }

        /**
         * 6 - Sending_Application_Address
         * 
         * Specifies the IP address of the application sending the data.
         */
        public string SendingApplicationAddress()
        {
            return Part(6);
        }

        /**
         * 7 - Byte_Count
         * 
         * Specifies the number of bytes to send. Valid values are 1 - 256.
         */
        public int ByteCount()
        {
            return IntPart(7);
        }

        /**
         * 8 - Data_Sent
         * 
         * Specifies the data that the application wants to send to the other 
         * application.
         */
        public string DataSent()
        {
            return Part(8);
        }

        public new void Process()
        {
            // TODO
        }
    }
}
