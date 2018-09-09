using MVVMSample.Infrastructure.Services;
using MVVMSample.ViewModels.Wizards.Product;

namespace MVVMSample.ViewModels.DesignTime
{
    /// <summary>
    /// Design time ViewModel with parameterless constructor
    /// </summary>
    public class DesignRequiredBoatFieldsViewModel : RequiredBoatFieldsViewModel
    {
        public DesignRequiredBoatFieldsViewModel() : base(new LocalizationService()) {}
    }
}