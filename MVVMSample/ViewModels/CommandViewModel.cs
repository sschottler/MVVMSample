using System.Windows.Input;
using MVVMSample.Infrastructure;
using MVVMSample.Infrastructure.Interfaces;

namespace MVVMSample.ViewModels
{
    /// <summary>
    /// ViewModel for menu commands 
    /// </summary>
    public class CommandViewModel : ViewModelBase
    {
        public CommandViewModel(string title, ICommand command, ILocalizationService localizationService) : base(localizationService)
        {
            Title = title;
            Command = command;
        }

        public string Title { get; private set; }
        public ICommand Command { get; private set; }
    }
}