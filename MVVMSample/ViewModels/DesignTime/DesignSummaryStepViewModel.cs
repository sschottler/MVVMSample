using MVVMSample.Infrastructure.Services;
using MVVMSample.ViewModels.Wizards;

namespace MVVMSample.ViewModels.DesignTime
{
    /// <summary>
    /// Design time ViewModel with parameterless constructor
    /// </summary>
    public class DesignSummaryStepViewModel : SummaryStepViewModel
    {
        public DesignSummaryStepViewModel() : base(new LocalizationService()) {}
    }
}