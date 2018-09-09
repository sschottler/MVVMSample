using MVVMSample.Infrastructure.Services;

namespace MVVMSample.ViewModels.DesignTime
{
    /// <summary>
    /// Only necessary because the runtime ViewModel doesn't have a parameterless constructor
    /// </summary>
    public class DesignLanguageSelectionViewModel : LanguageSelectionViewModel
    {
        public DesignLanguageSelectionViewModel() : base(new LocalizationService()) {}
    }
}