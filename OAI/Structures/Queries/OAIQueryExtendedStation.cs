using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Structures.Queries
{
    public class OAIQueryExtendedStation : OAIQuery
    {
        public override string ID()
        {
            return Extension;
        }

        /**
         * Station List
         * 
         * <Extension> Extension of the station.
         * <|Username|> Username assigned to the station.
         * <|Description|> Description assigned to the station.
         */

        /**
         * <Attendant> Extension of the station’s attendant.
         */
        public int Attendant;

        /**
         * <Is_An_Administrator> 
         *      0 = Station is not an administrator station.
         *      1 = Station is an administrator station.
         */

        public int Is_An_Administrator;

        /**
         * <Is_An_Attendant> 
         *      0 = Station is not an attendant.
         *      1 = Station is an attendant.
         */
        public int Is_An_Attendant;

        /**
         * <Day_COS_Flags> This value is in the form XXXX, where X is a hexadecimal
         *      number in the range 0-F. The least significant bit represents
         *      Day COS flag 1, and the most significant bit represents Day
         *      COS flag 16. A set bit indicates that the corresponding flag is
         *      enabled.
         */
        public int Day_COS_Flags;

        /**
         * <Night_COS_Flags> This value is in the form XXXX, where X is a hexadecimal
         *      number in the range 0-F. The least significant bit represents
         *      Night COS flag 1, and the most significant bit represents
         *      Night COS flag 16. A set bit indicates that the corresponding
         *      flag is enabled.
         */
        public int Night_COS_Flags;

        /**
         * <Voice_Mail_Extension> Voice mail extension of the station. If blank, 
         *      the station has no voice mail extension.
         */
        public int Voice_Mail_Extension;

        /**
         * <Device_Type> 
         *      0 Keyset Station Device         
         *      1 Single-Line Station Device    
         *      2 ACD/Hunt Group                
         *      3 Loop Start Trunk              
         *      4 Loop Start Trunk w/Caller ID  
         *      5 Ground Start Trunk            
         *      6 Ground Start Trunk w/Caller ID 
         *      7 DID Trunk                     
         *      8 E&M Trunk                     
         *      9 ISDN Trunk (Primary or Basic Rate) 
         *      
         *      10 Trunk Group                  
         *      11 Operator 
         *      12 Voice Mail 
         *      13 Other 
         *      14 Feature Code2, 3 
         *      15 Page Zone2 
         *      16 Page Port2 
         *      17 Off-Node Keyset Station Device2 
         *      18 Off-Node Single-Line Station Device2 
         *      19 Off-Node Hunt Group2 
         *      
         *      20 Off-Node Page Port2
         *      21 Off-Node Page Zone2
         *      22 Off-Node Voice Mail2
         *      23 ACD Agent2
         *      24 Unassociated Mailbox2
         *      25 Off-Node Unassociated Mailbox2
         *      26 BRI Station
         *      27 Off-Node BRI Station2
         *      28 MFC/R2 Trunk
         *      29 Modem1
         *      
         *      30 Off Node Modem4
         *      31 MGCP Phone
         *      32 MGCP Gateway and Phone
         *      33 SIP Trunk
         *      34 Phantom
         *      35 SIP Translator
         *      36 UC Advanced Softphone
         *      37 Configuration Assistant2
         *      38 SIP Phone
         *      39 OfficeLink Assistant
         *      
         *      40 Meet-Me Conference Assistant
         */
        public int Device_Type;

        public static string[] TYPES = new string[]
        {
            "Keyset Station Device",
            "Single-Line Station Device",
            "ACD/Hunt Group",
            "Loop Start Trunk",
            "Loop Start Trunk w/Caller ID",
            "Ground Start Trunk",
            "Ground Start Trunk w/Caller ID",
            "DID Trunk",
            "E&M Trunk ",
            "ISDN Trunk (Primary or Basic Rate)",

            "Trunk Group",
            "Operator",
            "Voice Mail",
            "Other",
            "Feature Code2, 3",
            "Page Zone2",
            "Page Port2",
            "Off-Node Keyset Station Device2",
            "Off-Node Single-Line Station Device2",
            "Off-Node Hunt Group2",

            "Off-Node Page Port2",
            "Off-Node Page Zone2",
            "Off-Node Voice Mail2",
            "ACD Agent2",
            "Unassociated Mailbox2",
            "Off-Node Unassociated Mailbox2",
            "BRI Station",
            "Off-Node BRI Station2",
            "MFC/R2 Trunk",
            "Modem1",

            "Off Node Modem4",
            "MGCP Phone",
            "MGCP Gateway and Phone",
            "SIP Trunk",
            "Phantom",
            "SIP Translator",
            "UC Advanced Softphone",
            "Configuration Assistant2",
            "SIP Phone",
            "OfficeLink Assistant",

            "Meet-Me Conference Assistant"
        };

        public String DeviceType()
        {
            if (0 > Device_Type || TYPES.Length < Device_Type)
            {
                return "Unknown";
            }

            return TYPES[Device_Type];
        }

        /**
         * <Mailbox_Node_Number> The node containing the station’s associated mailbox. 
         *      If zero, the station does not have an associated mailbox on any node.
         */
        public int Mailbox_Node_Number;

        /**
         * <Physical_Device_Type> The physical device connected to the communications 
         *      system.
         */
        public int Physical_Device_Type;
    }
}
