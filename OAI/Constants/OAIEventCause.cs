namespace OAI.Constants
{
    public class OAIEventCause
    {
        private OAIEventCause() { }

        public const int ACTIVE_MONITOR = 1; // 01 Active Monitor 
        public const int ALTERNATE = 2; // 02 Alternate (Reserved)
        public const int BUSY = 3; // 03 Busy
        
        public const int CALL_BACK = 4; // 04 Call Back (Reserved)
        public const int CALL_CANCELED = 5; // 05 Call Canceled (Reserved)
        public const int CALL_FORWARD_ALWAYS = 6; // 06 Call Forward Always (Reserved)
        public const int CALL_FORWARD_BUSY = 7; // 07 Call Forward Busy (Reserved)
        public const int CALL_FORAWRD_NO_ANSWER = 8; // 08 Call Forward No Answer
        public const int CALL_FORWARD = 9; // 09 Call Forward
        public const int CALL_NOT_ANSWERED = 10; // 10 Call Not Answered 
        public const int CALL_PICKUP = 11; // 11 Call Pickup
        
        public const int CAMP_ON = 12; // 12 Camp On
        public const int DESTINATION_NOT_AVAILABLE = 13; // 13 Destination Not Available
        public const int DO_NOT_DISTURB = 14; // 14 Do-Not-Disturb
        public const int INCOMPATIBLE_DESTINATION = 15; // 15 Incompatible Destination (Reserved)
        public const int INVALID_ACCOUNT_CODE = 16; // 16 Invalid Account Code (Reserved)
        public const int KEY_CONFERENCE = 17; // 17 Key Conference (Reserved) 
        public const int LOCKOUT = 18; // 18 Lockout (Reserved) 
        public const int MAINTENANCE = 19; // 19 Maintenance (Reserved)
        public const int NETWORK_CONGESTION = 20; // 20 Network Congestion (Reserved)

        public const int NETWORK_NOT_OBTAINABLE = 21; // 21 Network Not Obtainable (Reserved)
        public const int NEW_CALL = 22; // 22 New Call 

        public const int NO_AVAILABLE_AGENTS = 23; // 23 No Available Agents
        public const int OVERRRIDE = 24; // 24 Override
        public const int PARK = 25; // 25 Park (Reserved)
        public const int OVERFLOW = 26; // 26 Overflow
        public const int RECALL = 27; // 27 Recall
        public const int REDIRECTED = 28; // 28 Redirected
        public const int REORDER_TONE = 29; // 29 Reorder Tone
        public const int RESOURCE_NOT_AVAILABLE = 30; // 30 Resources Not Available
        public const int SILENT_MONITOR = 31; // 31 Silent Monitor
        public const int TRANSFER = 32; // 32 Transfer
        public const int TRUNKS_BUSY = 33; // 33 Trunks Busy
        public const int VOICE_UNIT_INITIATOR = 34; // 34 Voice Unit Initiator (Reserved)
        public const int ANSWERED = 35; // 35 Answered
        public const int NORMAL_CLEAR = 36; // 36 Normal Clear

        public const int HOLD_SYSTEM = 37; // 37 Hold – System
        public const int HOLD_FOR_TRANSFER = 38; // 38 Hold For Transfer
        public const int HOLD_FOR_CNF = 39; // 39 Hold For CNF

        public const int TRANSFER_ANNOUNCEMENT = 40; // 40 Transfer Announcement
        public const int CONFERENCE = 41; // 41 Conference
        public const int HOLD_INDIVIDUAL = 42; // 42 Hold – Individual
        public const int RECORD_A_CALL = 43; // 43 Record-A-Call
        public const int CALL_STOLEN = 44; // 44 Call Stolen

    }
}
