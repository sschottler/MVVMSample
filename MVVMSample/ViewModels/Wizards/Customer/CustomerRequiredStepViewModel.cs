using MVVMSample.Infrastructure.Interfaces;
using MVVMSample.Infrastructure.Wizard;

namespace MVVMSample.ViewModels.Wizards.Customer
{
    public class CustomerRequiredStepViewModel : WizardStepBaseViewModel
    {
        private string _firstName;
        private string _lastName;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RaisePropertyChanged(() => FirstName);
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged(() => LastName);
            }
        }

        public CustomerRequiredStepViewModel(ILocalizationService localizationService) : base(localizationService)
        {
            SetDisplayName();
        }

        protected override void OnCultureChanged(CultureChangedEventArgs args)
        {
            SetDisplayName();
        }

        private void SetDisplayName()
        {
            DisplayName = LocalizationService.GetLocalizedString("RequiredCustomerFields");
        }

        /// <summary>
        /// Validation logic for enabling/disabling Next button
        /// </summary>
        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName);
        }

        /// <summary>
        /// Called when start over is clicked
        /// </summary>
        public override void Clear()
        {
            FirstName = LastName = null;
        }
    }
}