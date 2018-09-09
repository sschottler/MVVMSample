using Microsoft.Win32;
using MVVMSample.Infrastructure.Interfaces;

namespace MVVMSample.Infrastructure.Services
{
    public class OpenFileDialogService : IOpenFileDialogService
    {
        public string OpenFileDialog(string initialDirectory)
        {
            return OpenFileDialog(string.Empty, string.Empty);
        }

        public string OpenFileDialog(string initialDirectory, string filter)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (!string.IsNullOrEmpty(initialDirectory))
                dialog.InitialDirectory = initialDirectory;

            if (!string.IsNullOrEmpty(filter))
                dialog.Filter = filter;

            bool? result = dialog.ShowDialog();

            if (result == true)
                return dialog.FileName;

            return string.Empty;
        }
    }
}