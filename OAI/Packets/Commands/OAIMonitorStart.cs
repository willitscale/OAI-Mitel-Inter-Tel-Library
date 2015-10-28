using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Commands
{
    /**
     * Monitor Start – _MS
     * 
     * Initiates event reporting for a device, calls via a device, or a call. After a port 
     * begins monitoring a call, it continues monitoring under the first monitor 
     * <Mon_Cross_Ref_ID> until that call clears out of the system. For example, if a call 
     * rings Device A, which is being monitored (calls via device), and is forwarded to 
     * Device B, which is also being monitored (calls via device), the events for the call 
     * will continue to be sent out under the <Mon_Cross_Ref_ID> for Device A. No events will 
     * be sent under the <Mon_Cross_Ref_ID> for Device B. There are five distinct types of 
     * monitor sessions available on the system: 
     *  Call: Provides event notification for the specified call until the call terminates 
     *      from the system. This type of monitor session generates all call-related events. 
     *      It does not generate feature- or agent-related events.
     *  Device: Provides event notification for the specified device (e.g., station, hunt 
     *      group, trunk group, voice mail application, agent ID, etc.). This type of monitor 
     *      session generates all call events (except the Call Cleared event), agent events, 
     *      and feature events.
     *  Calls via Device: Provides event notification for the life of every call that hits 
     *      a specified device.
     *  Call Termination: Creates a system-wide monitor that generates a Call Cleared event
     *      (see page 43) for every call in the system when the call terminates.
     *  System: Creates a system-wide monitor that generates all system events.
     * 
     * Using the Gateway, this command allows the application to specify a node format 
     * (e.g., 1: for Node 1) when performing monitor start all stations, all stations 
     * with resync, all trunks, or system. 
     * 
     * For OAI purposes, the Personal Router Device is a hunt group type that can be 
     * monitored like a hunt group. The Personal Router Device extension is non-dialable 
     * so that existing OAI applications do not receive events from the hunt group 
     * type without conscientiously knowing the impact of these events and the hunt 
     * group type. 
     * 
     * You can monitor the Personal Router Device like a hunt group. To monitor the 
     * Personal Router Device, change the extension from PP065 to a dialable extension 
     * in DB Programming On-Line Monitor Mode under Users – Dynamic Extension 
     * Express-Related Information. Once you change the Personal Router Device's 
     * extension from PP065 to the dialable extension, you should not change it back 
     * to PP065. 
     */
    class OAIMonitorStart : OAICommand
    {
        public const string CMD = "_MS";

        /**
         * Value <Monitor_Type> Valid Forms of <Affected_ID> 
         */

        // 0 Call – Call monitoring on specified call. Valid Call ID (cannot use expanded format).
        public const int MONITOR_TYPE_CALL = 0x00;

        // 1 Device – Device monitoring on specified device. Valid device (see page 25).
        public const int MONITOR_TYPE_DEVICE = 0x01;

        // 2 Calls Via Device – Call via Device monitoring on specified device. Valid Hunt Group device.
        public const int MONITOR_TYPE_CALL_VIA_DEVICE = 0x02;

        // 3 All Stations (Device-Type Monitoring) on all stations. Not used.
        public const int MONITOR_TYPE_ALL_STATIONS = 0x03;

        // 4 All Trunks (Device-Type Monitoring) on all trunks. Not used.
        public const int MONITOR_TYPE_ALL_TRUNKS = 0x04;

        // 5 Device with Resync – Device monitoring on specified device. Valid device (see page 25).
        public const int MONITOR_TYPE_DEVICE_WITH_RESYNC = 0x05;

        // 6 All Stations with Resync – Device monitoring on all stations. Not used.
        public const int MONITOR_TYPE_ALL_STATIONS_WITH_RESYNC = 0x06;

        // 7 Agent ID – Creates a device-type monitor wherever the agent logs in. Valid Agent ID.
        public const int MONITOR_TYPE_AGENT_ID = 0x07;

        // 8 Call Termination. Not used. Commands
        public const int MONITOR_TYPE_CALL_TERMINATED = 0x08;

        // 9 All Voice Mail Applications – Device-type monitor. Not used.
        public const int MONITOR_TYPE_ALL_VOICE_MAIL_APPLICATIONS = 0x09;

        // 10 All Private Network Trunks. Reserved for future use.
        public const int MONITOR_TYPE_ALL_PRIVATE_NETWORK_TRUNKS = 0xA0;

        // 11 System – System monitor. Not used
        public const int MONITOR_TYPE_SYSTEM = 0xA1;

        public OAIMonitorStart() : this(0, new string[0]) {}

        public OAIMonitorStart(int type, string[] filters) : this (null,type,filters){}

        public OAIMonitorStart(string affected, int type, string[] filters)
        {
            Command = CMD;

            // _MS,<InvokeID>,<Affected_ID>,<Monitor_Type>,<Filter_List><CR>
            Arguments = new string[filters.Length + 2];

            // Affected_ID
            // Indicates a phone, hunt/ACD group, modem (Axxess only), trunk group,
            // trunk, voice mail application, or agent ID. 
            Arguments[0] = (null == affected) ? "" : affected;

            // Monitor_Type
            // Identifies the requested monitor type (default is “1”), which can be one of
            // the values identified in the following table.
            Arguments[1] = type.ToString();

            // Filter_List
            for (int i = 0; i < filters.Length; i++)
            {
                Arguments[i + 2] = filters[i];
            }
        }

        public override bool Delayed()
        {
            return false;
        }

        public override void Block() { }

        public override void Release() { }
    }
}
