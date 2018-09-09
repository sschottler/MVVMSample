using System.Globalization;

namespace MVVMSample.Interfaces
{
    /// <summary>
    /// Interface for services to implement so we can encapsulate the details of 
    /// view construction from viewmodels and keep them loosely-coupled
    /// </summary>
    public interface IAppDialogService
    {
        CultureInfo ShowLanguageSelectionDialog();
        void ShowAboutDialog();
    }
}