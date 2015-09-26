using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAI.Structures.Queries
{
    public class OAIQueryExtendedFeature : OAIQuery
    {
        public override string ID()
        {
            return Feature_Number;
        }

        /**
         * 8 Feature List 
         */

        /**
         * <Feature_Code> Access code for the feature.
         * 
         * Can contain * and #
         */
        public string Feature_Code;

        /**
         * <Feature_Number> Logical number of the feature.
         */
        public string Feature_Number;

        /**
         * <|Feature_Name|> Description assigned to the feature.
         */
        public string Feature_Name;

        /*
         * <Is_Administrator_Feature>
         *      1 = Is an Administrator-Only Feature
         *      0 = Is not an Administrator-Only Feature
         */
        public int Is_Administrator_Feature;

        /**
         * <Is_Directory_Feature> 
         *      1 = Is a Directory Feature
         *      0 = Is not a Directory Feature
         */
        public int Is_Directory_Feature;

        /**
         * <Is_Diagnostic_Feature> 
         *      1 = Is a Diagnostic Feature
         *      0 = Is not a Diagnostic Feature
         */
        public int Is_Diagnostic_Feature;

        /**
         * <Is_Toggleable_Feature> 
         *      1 = Is a Toggleable Feature
         *      0 = Is not a Toggleable Feature
         */
        public int Is_Toggleable_Feature;
    }
}
