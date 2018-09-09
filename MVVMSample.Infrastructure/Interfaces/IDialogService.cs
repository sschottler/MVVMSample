using System.Windows;
using System.Windows.Controls;

namespace MVVMSample.Infrastructure.Interfaces
{
    public interface IDialogService
    {
        //could use some more overloads for syntactic elegance, but this is just a sample app
        bool? ShowDialog(
            IView view, 
            ViewModelBase viewModel, 
            string windowTitle,
            bool modal,
            Window parentWindow = null,
            bool showInTaskbar = false,
            WindowStartupLocation startupLocation = WindowStartupLocation.CenterOwner);
    }
}