using MVVMSample.Infrastructure.Services;
using MVVMSample.ViewModels.Wizards.Product;

namespace MVVMSample.ViewModels.DesignTime
{
    /// <summary>
    /// Design time ViewModel with OptionalFieldsViewModel set so the designer renders something
    /// </summary>
    public class DesignProductOptionalStepViewModel : ProductOptionalStepViewModel
    {
        public DesignProductOptionalStepViewModel() : base(new LocalizationService())
        {
            OptionalFieldsViewModel = new OptionalCarFieldsViewModel(new LocalizationService());
        }
    }
}