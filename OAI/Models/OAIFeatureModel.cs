using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OAI.Queues.Changes;

namespace OAI.Models
{
    public class OAIFeatureModel : OAIModel
    {

        /**
         * <Feature_Code> Access code for the feature.
         * 
         * Can contain * and #
         */
        private string _FeatureCode;
        public string FeatureCode
        {
            get
            {
                return _FeatureCode;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_FeatureCode))
                {
                    _FeatureCode = value;

                    // Trigger Feature update notification
                    OAIFeatureChangeQueue.Relay().Line = (null == _FeatureCode) ? value : _FeatureCode;
                }
            }
        }

        /**
         * <Feature_Number> Logical number of the feature.
         */
        private string _FeatureNumber;
        public string FeatureNumber
        {
            get
            {
                return _FeatureNumber;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_FeatureNumber))
                {
                    _FeatureNumber = value;

                    // Trigger Feature update notification
                    OAIFeatureChangeQueue.Relay().Line = _FeatureCode;
                }
            }
        }


        /**
         * <|Feature_Name|> Description assigned to the feature.
         */
        private string _FeatureName;
        public string FeatureName
        {
            get
            {
                return _FeatureName;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (null == value || 0 != value.CompareTo(_FeatureName))
                {
                    _FeatureName = value;

                    // Trigger Feature update notification
                    OAIFeatureChangeQueue.Relay().Line = _FeatureCode;
                }
            }
        }

        /*
         * <Is_Administrator_Feature>
         *      1 = Is an Administrator-Only Feature
         *      0 = Is not an Administrator-Only Feature
         */
        private int _IsAdministratorFeature;
        public int IsAdministratorFeature
        {
            get
            {
                return _IsAdministratorFeature;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (value != _IsAdministratorFeature)
                {
                    _IsAdministratorFeature = value;

                    // Trigger Feature update notification
                    OAIFeatureChangeQueue.Relay().Line = _FeatureCode;
                }
            }
        }

        /**
         * <Is_Directory_Feature> 
         *      1 = Is a Directory Feature
         *      0 = Is not a Directory Feature
         */
        private int _IsDirectoryFeature;
        public int IsDirectoryFeature
        {
            get
            {
                return _IsDirectoryFeature;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (value != _IsDirectoryFeature)
                {
                    _IsDirectoryFeature = value;

                    // Trigger Feature update notification
                    OAIFeatureChangeQueue.Relay().Line = _FeatureCode;
                }
            }
        }

        /**
         * <Is_Diagnostic_Feature> 
         *      1 = Is a Diagnostic Feature
         *      0 = Is not a Diagnostic Feature
         */
        private int _IsDiagnosticFeature;
        public int IsDiagnosticFeature
        {
            get
            {
                return _IsDiagnosticFeature;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (value != _IsDiagnosticFeature)
                {
                    _IsDiagnosticFeature = value;

                    // Trigger Feature update notification
                    OAIFeatureChangeQueue.Relay().Line = _FeatureCode;
                }
            }
        }

        /**
         * <Is_Toggleable_Feature> 
         *      1 = Is a Toggleable Feature
         *      0 = Is not a Toggleable Feature
         */
        private int _IsToggleableFeature;
        public int IsToggleableFeature
        {
            get
            {
                return _IsToggleableFeature;
            }

            set
            {
                // Only update/notify if a change has actually been made!
                if (value != _IsToggleableFeature)
                {
                    _IsToggleableFeature = value;

                    // Trigger Feature update notification
                    OAIFeatureChangeQueue.Relay().Line = _FeatureCode;
                }
            }
        }
    }
}
