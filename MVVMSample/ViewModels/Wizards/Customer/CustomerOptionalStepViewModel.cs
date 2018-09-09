using MVVMSample.Infrastructure.Interfaces;
using MVVMSample.Infrastructure.Wizard;

namespace MVVMSample.ViewModels.Wizards.Customer
{
    public class CustomerOptionalStepViewModel : WizardStepBaseViewModel
    {
        private int? _age;
        private string _city;

        public int? Age
        {
            get { return _age; }
            set
            {
                _age = value;
                RaisePropertyChanged(() => Age);
            }
        }

        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                RaisePropertyChanged(() => City);
            }
        }

        public CustomerOptionalStepViewModel(ILocalizationService localizationService) : base(localizationService)
        {
            SetDisplayName();
        }

        protected override void OnCultureChanged(CultureChangedEventArgs args)
        {
            SetDisplayName();
        }

        private void SetDisplayName()
        {
            DisplayName = LocalizationService.GetLocalizedString("OptionalCustomerFields");
        }

        /// <summary>
        /// Called when Start Over is clicked
        /// </summary>
        public override void Clear()
        {
            City = null;
            Age = null;
        }
    }
}