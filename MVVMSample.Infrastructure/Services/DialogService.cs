using System.Windows;
using System.Windows.Controls;
using MVVMSample.Infrastructure.Interfaces;

namespace MVVMSample.Infrastructure.Services
{
    public class DialogService : IDialogService
    {
        /// <summary>
        /// Helper method for opening windows. Don't want to call this directly from ViewModels since it requires a 
        /// UserControl view parameter, which would tightly-couple the ViewModel to the User Interface. 
        /// Instead, AppDialogServices or Controllers should interact with it
        /// </summary>
        public bool? ShowDialog(
            IView view, 
            ViewModelBase viewModel,
            string windowTitle,
            bool modal,
            Window parentWindow = null,
            bool showInTaskbar = false,
            WindowStartupLocation startupLocation = WindowStartupLocation.CenterOwner)
        {
            Window container = new Window
            {
                Content = view
            };

            if (parentWindow != null)
                container.Owner = parentWindow;

            container.WindowStartupLocation = startupLocation;
            container.Title = windowTitle;
            container.ShowInTaskbar = showInTaskbar;
            container.SizeToContent = SizeToContent.WidthAndHeight;

            if (viewModel != null)
            {
                view.DataContext = viewModel;
                viewModel.RequestClose += (s, args) => container.Close();
            }

            if (modal)
                return container.ShowDialog();

            container.Show();
            return true;
        }
    }
}