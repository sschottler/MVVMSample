using MVVMSample.Infrastructure;
using MVVMSample.Infrastructure.Interfaces;

namespace MVVMSample.ViewModels.Wizards.Product
{
    /// <summary>
    /// base class for RequiredCarFieldsViewModel and RequiredBoatFieldsViewModel. Both need to 
    /// have an IsValid() method for determining whether the Next button should be enabled
    /// </summary>
    public abstract class RequiredFieldsViewModel : ViewModelBase
    {
        protected RequiredFieldsViewModel(ILocalizationService localizationService) : base(localizationService) {}

        public abstract bool IsValid();
    }
}