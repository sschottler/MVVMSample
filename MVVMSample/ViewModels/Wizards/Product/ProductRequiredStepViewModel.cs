using MVVMSample.Infrastructure.Interfaces;
using MVVMSample.Infrastructure.Wizard;

namespace MVVMSample.ViewModels.Wizards.Product
{
    public class ProductRequiredStepViewModel : WizardStepBaseViewModel
    {
        private RequiredFieldsViewModel _requiredFieldsViewModel;

        /// <summary>
        /// This is set by the parent wizard (ProductWizardViewModel) based on the selected type (Car or Boat)
        /// </summary>
        public RequiredFieldsViewModel RequiredFieldsViewModel
        {
            get { return _requiredFieldsViewModel; }
            set
            {
                _requiredFieldsViewModel = value;
                RaisePropertyChanged(() => RequiredFieldsViewModel);
            }
        }

        public ProductRequiredStepViewModel(ILocalizationService localizationService) : base(localizationService)
        {
            SetDisplayName();
        }

        public override bool IsValid()
        {
            //forward the isvalid() call to the RequiredFieldsViewModel (Boat or Car will handle their own validation)
            if (RequiredFieldsViewModel != null)
                return RequiredFieldsViewModel.IsValid();

            return false;
        }

        public override void Clear()
        {
            RequiredFieldsViewModel = null;
        }

        protected override void OnCultureChanged(CultureChangedEventArgs args)
        {
            SetDisplayName();
        }

        private void SetDisplayName()
        {
            DisplayName = LocalizationService.GetLocalizedString("RequiredProductFields");
        }
    }
}