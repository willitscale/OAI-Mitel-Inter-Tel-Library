using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Commands
{
    /**
     * Query List Extended – _QX
     * 
     * Provides lists of various types, depending on the specified list number.
     */
    public class OAIQueryListExtended : OAICommand
    {
        public const string CMD = "_QX";

        /**
         * Val. 	Description 		Bit # 		Field 						Hex
         * 1 		Station List 		N/A 		<Extension> 				N/A
         * 								0 			<|Username|> 				1
         * 								1  			<|Description|> 			2
         * 								2  			<Attendant> 				4
         * 								3  			<Is_An_Adminstrator> 		8
         * 								4  			<Is_An_Attendant> 			10
         * 								5  			<Day_COS_Flags> 			20
         * 								6  			<Night_COS_Flags> 			40
         * 								7  			<Voice_Mail_Extension> 		80
         * 								8  			<Device_Type> 				100
         * 								9 			<Mailbox_Node_Number> 		200
         * 								10 			<Physical_Device_Type>   	400
         */
        public const int LIST_TYPE_STATION_LIST = 0x01;

        public const int MASK_STATION_LIST_USER = 0x001;
        public const int MASK_STATION_LIST_DESC = 0x002;
        public const int MASK_STATION_LIST_ATTEND = 0x004;
        public const int MASK_STATION_LIST_IS_ADMIN = 0x008;
        public const int MASK_STATION_LIST_IS_ATTEND = 0x010;
        public const int MASK_STATION_LIST_DAY_FLAGS = 0x020;
        public const int MASK_STATION_LIST_NIGHT_FLAGS = 0x040;
        public const int MASK_STATION_LIST_VOICE_MAIL = 0x080;
        public const int MASK_STATION_LIST_DEVICE_TYPE = 0x100;
        public const int MASK_STATION_LIST_MAILBOX_NODE = 0x200;
        public const int MASK_STATION_LIST_PHYSICAL_DEVICE = 0x400;

        /**
         * Val. 	Description 		Bit # 		Field 						Hex
         * 2 		Trunk List 			N/A 		<Extension>					N/A
         *                              0 			<|Username|> 				1
         *                              1 			<|Description|> 			2
         *                              2 			<Answer_Supervision_Type> 	4
         */
        public const int LIST_TYPE_TRUNK_LIST = 0x02;

        public const int MASK_TRUNK_LIST_USER = 0x001;
        public const int MASK_TRUNK_LIST_DESC = 0x002;
        public const int MASK_TRUNK_LIST_ANS_SUPERVISION = 0x004;

        /**
         * Val. 	Description 		Bit # 		Field 						Hex
         * 3 		Hunt Group List 	N/A 		<Extension> 				N/A
         *                              0 			<|Username|> 				1
         *                              1 			<|Description|> 			2
         *                              2 			<Hunt_Group_Type> 			4
         *                              3 			<Number_Of_Members> 		8
         *                              4 			<Mailbox_Node_Number> 		10
         */
        public const int LIST_TYPE_HUNT_GROUP_LIST = 0x03;

        public const int MASK_HUNT_GROUP_LIST_USER = 0x001;
        public const int MASK_HUNT_GROUP_LIST_DESC = 0x002;
        public const int MASK_HUNT_GROUP_LIST_HUNT_GROUP = 0x004;
        public const int MASK_HUNT_GROUP_LIST_MEMBERS = 0x008;
        public const int MASK_HUNT_GROUP_LIST_MAILBOX = 0x010;

        /** 
         * Val. 	Description 		Bit # 		Field 						Hex
         * 4 		Trunk Group List 	N/A 		<Extension>					N/A
         *                              0 			<|Username|> 				1
         *                              1 			<|Description|> 			2
         *                              2 			<Number_Of_Members> 		4
         */
        public const int LIST_TYPE_TRUNK_GROUP_LIST = 0x04;

        public const int MASK_HUNT_TRUNK_LIST_USER = 0x001;
        public const int MASK_HUNT_TRUNK_LIST_DESC = 0x002;
        public const int MASK_HUNT_TRUNK_LIST_MEMBERS = 0x004;

        /**
         * Val. 	Description 		Bit # 		Field 						Hex   
         * 5 		Page Zone List 		N/A 		<Extension> 				N/A
         *                              0 			<|Username|>                1
         *                              1 			<|Description|>             2
         *                              2 			<Number_Of_Members>         4
         */
        public const int LIST_TYPE_PAGE_ZONE_LIST = 0x05;

        public const int MASK_PAGE_ZONE_LIST_USER = 0x001;
        public const int MASK_PAGE_ZONE_LIST_DESC = 0x002;
        public const int MASK_PAGE_ZONE_LIST_MEMBERS = 0x004;

        /**
         * Val. 	Description 		Bit # 		Field 						Hex   
         * 6        Page Port List      N/A         <Extension>                 N/A
         * 								0 			<|Username|>                1
         * 								1 			<|Description|>             2
         */
        public const int LIST_TYPE_PAGE_PORT_LIST = 0x06;

        public const int MASK_PAGE_PORT_LIST_USER = 0x001;
        public const int MASK_PAGE_PORT_LIST_DESC = 0x002;

        /**
         * Val. 	Description 		Bit # 		Field 						Hex   
         * 7 		Unassociated Voice 	N/A 		<Mailbox_Number>			N/A
         *          Mailbox List
         *                              0           <|Username|>                1
         *                              1           <|Description|>             2
         */
        public const int LIST_TYPE_VOICE_MAILBOX_LIST = 0x06;

        public const int MASK_VOICE_MAILBOX_LIST_USER = 0x001;
        public const int MASK_VOICE_MAILBOX_LIST_DESC = 0x002;

        /**
         * Val. 	Description 		Bit # 		Field 						Hex   
         * 8        Feature List        N/A         <Feature_Code>              N/A
         *                              0           <Feature_Number> (Logical)  1
         *                              1           <Feature_Name>              2
         *                              2           <Is_Administrator_Feature>  4
         *                              3           <Is_Directory_Feature>      8
         *                              4           <Is_Diagnostic_Feature>     10
         *                              5           <Is_Toggleable_Feature>     20
         */
        public const int LIST_TYPE_FEATURE_LIST = 0x08;

        public const int MASK_FEATURE_LIST_FEAT_NUM = 0x001;
        public const int MASK_FEATURE_LIST_FEAT_NAME = 0x002;
        public const int MASK_FEATURE_LIST_IS_ADMIN = 0x004;
        public const int MASK_FEATURE_LIST_IS_DIR = 0x008;
        public const int MASK_FEATURE_LIST_IS_DIAG = 0x010;
        public const int MASK_FEATURE_LIST_IS_TOGGLE = 0x020;

        /** 
         * Val. 	Description 		Bit # 		Field 						Hex   
         * 9        Speed-Dial Bin List N/A         <Bin_Number>                N/A
         *                               0          <Bin_Contents>              1
         *                               1          <|Bin_Name|>                2
         */
        public const int LIST_TYPE_SPEED_DIAL_LIST = 0x09;

        public const int MASK_SPEED_DIAL_LIST_CONTENTS = 0x001;
        public const int MASK_SPEED_DIAL_LIST_NAME = 0x002;

        /**
         * Val. 	Description 		Bit # 		Field 						Hex   
         * 10       DND Message List    N/A         <DND_Message_Number>        N/A
         *                              0           <|DND_Message_Text|>        1
         */
        public const int LIST_TYPE_DND_LIST = 0x0A;

        public const int MASK_DND_LIST_MESSAGE = 0x001;

        /**
         * Val. 	Description 		Bit # 		Field 						Hex   
         * 11       Reminder Message    N/A         <Reminder_Message_Number>   N/A
         *          List 
         *                               0          <|Reminder_Message_Text|>   1
         */
        public const int LIST_TYPE_REMINDER_LIST = 0x0B;

        public const int MASK_REMINDER_LIST_MESSAGE = 0x001;

        /**
         * Val. 	Description 		Bit # 		Field 						Hex   
         * 12       Voice Mail          N/A         <Extension>                 N/A
         *          Application List
         *                              0           <|Username|>                1
         *                              1           <|Description|>             2
         *                              2           <Application_Type>          4
         */

        /**
         * Val. 	Description 		Bit # 		Field 						Hex   
         * 13       Private Networking  N/A         <Extension>                 N/A
         *          Trunk List
         *                              0           <|Username|>                1
         *                              1           <|Description|>             2
         */

        /**
         * Val. 	Description 		Bit # 		Field 						Hex   
         * 14       Private Networking  N/A         <Extension>                 N/A
         *          Trunk Group List
         *                              0           <|Username|>                1
         *                              1           <|Description|>             2
         *                              2           <Number_Of_Members>         4
         */

        /**
         * Val. 	Description 		Bit # 		Field 						Hex   
         * 15       Off-Node Device     N/A         <Node: Extension>           N/A
         *          List 
         *                              0           <Device_Type>               1
         *                              1           <|Username|>                2
         *                              2           <|Description|>             4
         *                              3           <Mailbox_Node_Number>       8
         */

        /**
         * Val. 	Description 		Bit # 		Field 						Hex   
         * 16       System COS Flag     N/A         <Flag_Number>               N/A
         *                              0           <|Description|>             1
         */

        /**
         * Val. 	Description 		Bit # 		Field 						Hex
         * 17       ACD Agent           N/A         <Agent_ID>                  N/A
         * 	                            0           <|Description|>             1
         */
        public const int LIST_TYPE_ACD_AGENT = 0x11;

        public const int MASK_ACD_AGENT_DESC = 0x01;

        /**
         * Val. 	Description 		Bit # 		Field 						Hex   
         * 18       Node Data           N/A         <Network_Node_Number>       N/A
         *                              0           <|Description|>             1
         *                              1           <Networking_Enabled/Disabled> 2
         *                              2           <Protocol_Version>          4
         *                              3           <KSU_SW_Version>            8
         *                              4           <Premium_Feature_Status>    10
         *                              5           <Country_Code>              20
         *                              6           <TCPIP_Indicator>           40
         *                              7           <Voice_Mail_Status>         80
         *                              8           <Max_Parties_in_Conference> 100
         */

        /**
         * Val. 	Description 		Bit # 		Field 						Hex   
         * 19       Network Information N/A         <Network_Node_Number>       N/A
         *                              0           <|Node_Description|>        1
         *                              1           <|IP_Address|>              2
         *                              2           <TCP_Port>                  4
         *                              3           <|Redundant_IP_Address|>    8
         *                              4           <Redundant_TCP_Port>        10
         *                              5           <System_OAI_Sockets_Enabled> 20
         *                              6           <Max_Supported_System_OAI_Sockets> 40
         *                              7           <Protocol_Version>          80
         *                              8           <KSU_SW_Version>            100
         *                              9           <Premium_Feature_Status>    200
         */

        public const int LIST_TYPE_NETWORK_NODE = 0x13;

        /**
         * Val. 	Description 		Bit # 		Field 						Hex   
         * 20       IPRC and            N/A         <Extension>                 N/A
         *          IP Connections
         *                              0           <|Username|>                1
         *                              1           <|Description|>             2
         */

        /**
         * Val. 	Description 		Bit # 		Field 						Hex   
         * 21       User                N/A         <Main_Extension>            N/A
         *                              0           <|First_Name|>              1
         *                              1           <|Last_Name|>               2
         *                              2           <|Email_Address|>           4
         *                              3           <|Login|>                   8
         *                              4           <Is_Personal_Call_Routing_Enabled> 10
         *                              5           <Mobile_Email_Address>      20
         */

        /**
         * Val. 	Description 		Bit # 		Field 						Hex   
         * 22       Assistant List      N/A         <Extension>                 N/A
         *                              0           <|Username|>                1
         *                              1           <|Description|>             2
         *                              2           <Device_Type>               4
         */

        public OAIQueryListExtended(int type, int mask) : this(0, type, mask) { }

        public OAIQueryListExtended(int affected, int type, int mask)
        {
            Command = CMD;

            // _QX,<InvokeID>,<Affected_Ext>,<List_Type>,<Entity_Field_Mask>,
            // <Response_Mode>,<Response_Size><CR>
            Arguments = new string[5];

            // Affected_Ext
            // Specifies the system node, using the Node: format (e.g., 1:), any valid
            // extension on the system node, or it can be blank.
            Arguments[0] = ( 0 == affected ) ? "" : affected.ToString();

            // List_Type
            // Indicates the type of list being queried
            Arguments[1] = type.ToString();

            // Entity_Field_Mask
            // Returns a list of entities where each entity may contain several fields
            // of information.
            Arguments[2] = mask.ToString("X");

            // Response_Mode
            // Single Message Mode (1): The entire list is sent in a single message.
            // Multiple Message Mode (2): The list is generated in item groups.
            Arguments[3] = "1";

            // Response_Size
            // Defines the maximum number of elements, up to the maximum number
            // of system extensions, to be included in a response.
            Arguments[4] = "1";
        }

        public override bool Delayed()
        {
            return false;
        }

        public override void Block() { }

        public override void Release() { }
    }
}
