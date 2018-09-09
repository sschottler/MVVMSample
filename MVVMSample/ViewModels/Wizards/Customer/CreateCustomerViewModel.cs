using MVVMSample.Infrastructure.Interfaces;
using MVVMSample.Model.Interfaces;

namespace MVVMSample.ViewModels.Wizards.Customer
{
    public class CreateCustomerViewModel : WorkspaceViewModel
    {
        public override string DisplayName
        {
            get { return GetLocalizedString("CreateCustomer"); }
        }

        public CustomerWizardViewModel CustomerWizardViewModel
        {
            get; set;
        }

        public CreateCustomerViewModel(ICustomerRepository customerRepository, IMessageBoxService messageBoxService, ILocalizationService localizationService)
            : base(localizationService)
        {
            CustomerWizardViewModel = new CustomerWizardViewModel(customerRepository, messageBoxService, localizationService);

            CustomerWizardViewModel.Cancelled += (s, args) => OnRequestClose();
            CustomerWizardViewModel.Finished += (s, args) => OnRequestClose();
        }
    }
}