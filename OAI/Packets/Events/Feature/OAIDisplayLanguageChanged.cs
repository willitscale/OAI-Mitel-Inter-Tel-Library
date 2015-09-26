using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Packets.Events.Feature
{
    /**
     * Display Language Changed – DLC
     * 
     * Occurs whenever the language on a station changes. The station language can be 
     * changed in Database Programming, or the user can enter the Change Language feature 
     * code (default is 301) at the station.
     * 
     * DLC,<Resync_Code>,<Mon_Cross_Ref_ID>,<Subject_Ext>,<New_Language>,
     * <Previous_Language>
     */
    public class OAIDisplayLanguageChanged : OAIFeature
    { 
        public const string EVENT = "DLC";

        public OAIDisplayLanguageChanged(string[] parts) : base(parts) { }
        public OAIDisplayLanguageChanged(byte[] bytes) : base(bytes) { }

        /**
         * 4 - Subject_Ext
         * 
         * Specifies the device sending the event.
         */
        public string SubjectExt()
        {
            return Part(4);
        }

        /**
         * 5 - New_Language
         * 
         * Indicates the new language assigned to the station (0 = Primary; 
         * 1 = Secondary).
         */
        public int NewLanguage()
        {
            return IntPart(5);
        }

        /**
         * 6 - Previous_Language
         * 
         * Indicates the language used at the station before the change 
         * (0 = Primary; 1 = Secondary).
         */
        public int PreviousLanguage()
        {
            return IntPart(6);
        }

        public new void Process()
        {
            // TODO
        }
    }
}
