using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Packets;
using OAI.Packets.Commands;
using OAI.Queues;
using OAI.Workers;
using OAI.Configuration;

namespace OAI.Sequences
{
    public class OAIGlobalSequence : OAISequence
    {
        public OAIGlobalSequence(OAIConfig config) : base(config) { }

        public override void Step(OAIEvent evt)
        {
            // Don't initialise if already connected!
            if (Connected)
            {
                return;
            }

            switch (Stage)
            {
                case STAGE_START:
                {
                    // Confirm we connected to the phone system
                    // 000,RS,0,02:04,070515,V10.20,V6.0.11.113,3,0,1,3,1

                    // Then confirm we have finished syncing
                    // 001,RD,0,
                    NodeAware(evt, STAGE_NODE_AWARE);
                    return;
                }

                case STAGE_NODE_AWARE:
                {
                    // Confirming we are node aware
                    // 002,CF,_NA,1,0,1
                    StationMonitor(evt, STAGE_STATION_MONITOR);
                    return;
                }

                case STAGE_STATION_MONITOR:
                {
                    // Confirming we are monitoring all stations
                    // 003,CF,_MS,2,0,0242 ... 000076,1761,000077,1762 ... 
                    // ... A:00FC,C:FFFE,F:1FFF
                    SystemMonitor(evt, STAGE_SYSTEM_MONITOR);
                    return;
                }

                case STAGE_SYSTEM_MONITOR:
                {
                    // Confirming we are monitoring calls by device
                    // 004,CF,_MS,3,0,0002,000243,1:,000244,2:,S:00FE
                    QueryAgents(evt, STAGE_SYSTEM_QUERY_AGENTS);
                    return;
                }

                case STAGE_SYSTEM_QUERY_AGENTS:
                {
                    // 
                    // 
                    QueryStations(evt, STAGE_SYSTEM_QUERY_STATIONS);
                    return;
                }

                case STAGE_SYSTEM_QUERY_STATIONS:
                {
                    // 
                    // 
                    QueryHuntGroups(evt, STAGE_SYSTEM_QUERY_HUNTS);
                    break;
                }

                case STAGE_SYSTEM_QUERY_HUNTS:
                {
                    // 
                    // 
                    QueryFeatures(evt, STAGE_SYSTEM_QUERY_FEATURES);
                    break;
                }

                case STAGE_SYSTEM_QUERY_FEATURES:
                {
                    // 
                    // 
                    QueryDND(evt, STAGE_SYSTEM_QUERY_AGENT);
                    break;
                }

                case STAGE_SYSTEM_QUERY_AGENT:
                {
                    QueryAgent(evt, STAGE_FINISHED);
                    break;
                }

                // All setup events finished
                case STAGE_FINISHED:
                {
                    Finished(evt);

                    // Build the base data set
                    if (Connected)
                    {
                        DataBuilt = true;
                    }

                    break;
                }
            }
        }
    }
}
