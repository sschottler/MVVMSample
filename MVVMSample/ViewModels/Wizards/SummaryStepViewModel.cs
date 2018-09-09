using MVVMSample.Infrastructure.Interfaces;
using MVVMSample.Infrastructure.Wizard;
using MVVMSample.Model;

namespace MVVMSample.ViewModels.Wizards
{
    /// <summary>
    /// This step is shared between both wizards
    /// </summary>
    public class SummaryStepViewModel : WizardStepBaseViewModel
    {
        private ResponseCode _response = ResponseCode.Success;

        /// <summary>
        /// This enum response is databound to the UI and translated by the localization provider:
        /// </summary>
        public ResponseCode Response
        {
            get { return _response; }
            set
            {
                _response = value;
                RaisePropertyChanged(() => Response);
            }
        }

        public SummaryStepViewModel(ILocalizationService localizationService)
            : base(localizationService)
        {
            //disable start over, back, and cancel buttons:
            AllowStartOver = AllowBackNavigation = AllowCancel = false;
            SetDisplayName();
        }

        protected override void OnCultureChanged(CultureChangedEventArgs args)
        {
            //refresh DisplayName when culture changes:
            SetDisplayName();
        }

        private void SetDisplayName()
        {
            DisplayName = LocalizationService.GetLocalizedString("Summary");
        }
    }
}