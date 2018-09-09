using MVVMSample.Infrastructure.Services;
using MVVMSample.ViewModels.Wizards.Customer;

namespace MVVMSample.ViewModels.DesignTime
{
    /// <summary>
    /// Wouldn't need this class if the regular runtime ViewModel had a 
    /// parameterless constructor that would allow the designer to create it
    /// </summary>
    public class DesignCreateCustomerViewModel : CreateCustomerViewModel
    {
        public DesignCreateCustomerViewModel() : base(null, null, new LocalizationService()) {}
    }
}