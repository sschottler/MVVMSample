using System.Windows.Forms;
using MVVMSample.Infrastructure.Interfaces;
using MVVMSample.Infrastructure.Wizard;
using MVVMSample.Model;
using MVVMSample.Model.Interfaces;
using Customers = MVVMSample.Model.Customers;

namespace MVVMSample.ViewModels.Wizards.Customer
{
    public class CustomerWizardViewModel : WizardBaseViewModel
    {  
        private ICustomerRepository _customerRepository;
        private IMessageBoxService _messageBoxService;

        public CustomerRequiredStepViewModel CustomerRequiredStepViewModel
        {
            get { return GetStep<CustomerRequiredStepViewModel>(); }
        }

        public CustomerOptionalStepViewModel CustomerOptionalStepViewModel
        {
            get { return GetStep<CustomerOptionalStepViewModel>(); }
        }

        public SummaryStepViewModel SummaryStepViewModel
        {
            get { return GetStep<SummaryStepViewModel>(); }
        }

        public CustomerWizardViewModel(ICustomerRepository customerRepository, IMessageBoxService messageBoxService, ILocalizationService localizationService)
            : base(localizationService)
        {
            _customerRepository = customerRepository;
            _messageBoxService = messageBoxService;

            Steps.Add(new CustomerRequiredStepViewModel(localizationService));
            Steps.Add(new CustomerOptionalStepViewModel(localizationService));
            Steps.Add(new SummaryStepViewModel(localizationService));
        }

        protected override void OnNavigating(WizardNavigationEventArgs eventArgs)
        {
            if (eventArgs.CurrentStep is CustomerOptionalStepViewModel && eventArgs.NavigationType == WizardNavigationType.Next)
            {
                string title = LocalizationService.GetLocalizedString("CustomerConfirmation");
                string message = LocalizationService.GetLocalizedString("CustomerConfirmationMessage");

                //show Dialog box to confirm creation:
                if (_messageBoxService.ShowMessageBox(title, message, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Pause navigation (you must eventually call ContinueNavigation() or CancelNavigation() when you pause navigation)
                    eventArgs.NavigationAction = WizardNavigationAction.Pause;
                    CreateCustomer();
                }
                else
                    eventArgs.NavigationAction = WizardNavigationAction.Cancel;
            }
        }

        private void CreateCustomer()
        {
            //show animated activity indicator:
            IsBusy = true;
            BusyMessage = LocalizationService.GetLocalizedString("CreatingCustomer");
            
            Customers.Customer customer = new Customers.Customer(CustomerRequiredStepViewModel.FirstName, CustomerRequiredStepViewModel.LastName)
            {
                City = CustomerOptionalStepViewModel.City
            };

            if (CustomerOptionalStepViewModel.Age.HasValue)
                customer.Age = CustomerOptionalStepViewModel.Age.Value;

            _customerRepository.CreateCustomer(customer, CustomerCreatedCallback);
        }

        private void CustomerCreatedCallback(ResponseCode response)
        {
            IsBusy = false;
            SummaryStepViewModel.Response = response;
            ContinueNavigation();
        }
    }
}