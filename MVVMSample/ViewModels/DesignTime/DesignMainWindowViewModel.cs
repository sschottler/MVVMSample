using MVVMSample.Infrastructure.Services;
using MVVMSample.ViewModels.Wizards.Customer;
using MVVMSample.ViewModels.Wizards.Product;

namespace MVVMSample.ViewModels.DesignTime
{
    /// <summary>
    /// Design time viewmodel that allows you to fake data for the designer
    /// </summary>
    public class DesignMainWindowViewModel : MainWindowViewModel
    {
        public DesignMainWindowViewModel() : base(null, null, null, null, null, null, null, new LocalizationService())
        {
            //show some workspaces in the tab control in the designer:
            Workspaces.Add(new CreateCustomerViewModel(null, null, new LocalizationService()));
            Workspaces.Add(new CreateProductViewModel(null, new LocalizationService()));
        }
    }
}