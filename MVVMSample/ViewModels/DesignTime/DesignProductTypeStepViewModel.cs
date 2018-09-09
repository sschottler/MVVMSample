using MVVMSample.Infrastructure.Services;
using MVVMSample.ViewModels.Wizards.Product;

namespace MVVMSample.ViewModels.DesignTime
{
    /// <summary>
    /// Design time ViewModel with parameterless constructor
    /// </summary>
    public class DesignProductTypeStepViewModel : ProductTypeStepViewModel
    {
        public DesignProductTypeStepViewModel() : base(new LocalizationService()) {}
    }
}