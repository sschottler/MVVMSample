using MVVMSample.Infrastructure.Services;
using MVVMSample.ViewModels.Wizards.Product;

namespace MVVMSample.ViewModels.DesignTime
{
    /// <summary>
    /// Design time ViewModel with the RequiredFieldsViewModel set so something renders in the designer
    /// </summary>
    public class DesignProductRequiredStepViewModel : ProductRequiredStepViewModel
    {
        public DesignProductRequiredStepViewModel() : base(new LocalizationService())
        {
            RequiredFieldsViewModel = new RequiredCarFieldsViewModel(new LocalizationService());
        }
    }
}