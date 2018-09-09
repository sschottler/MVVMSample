using MVVMSample.Infrastructure.Services;
using MVVMSample.ViewModels.Wizards.Product;

namespace MVVMSample.ViewModels.DesignTime
{
    /// <summary>
    /// Only necessary because the runtime ViewModel doesn't have a parameterless constructor
    /// </summary>
    public class DesignCreateProductViewModel : CreateProductViewModel
    {
        public DesignCreateProductViewModel() : base(null, new LocalizationService()) {}
    }
}