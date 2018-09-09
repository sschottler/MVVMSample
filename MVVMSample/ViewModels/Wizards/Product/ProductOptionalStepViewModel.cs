using MVVMSample.Infrastructure;
using MVVMSample.Infrastructure.Interfaces;
using MVVMSample.Infrastructure.Wizard;

namespace MVVMSample.ViewModels.Wizards.Product
{
    public class ProductOptionalStepViewModel : WizardStepBaseViewModel
    {
        private ViewModelBase _optionalFieldsViewModel;

        /// <summary>
        /// This is set by the parent wizard (ProductwizardViewModel) based on the selected type (Boat or Car)
        /// </summary>
        public ViewModelBase OptionalFieldsViewModel
        {
            get { return _optionalFieldsViewModel; }
            set
            {
                _optionalFieldsViewModel = value;
                RaisePropertyChanged(() => OptionalFieldsViewModel);
            }
        }

        public ProductOptionalStepViewModel(ILocalizationService localizationService) : base(localizationService)
        {
            SetDisplayName();
        }

        public override void Clear()
        {
            OptionalFieldsViewModel = null;
        }

        protected override void OnCultureChanged(CultureChangedEventArgs args)
        {
            //Refresh the DisplayName when language changes
            SetDisplayName();
        }

        private void SetDisplayName()
        {
            DisplayName = LocalizationService.GetLocalizedString("OptionalProductFields");
        }
    }
}