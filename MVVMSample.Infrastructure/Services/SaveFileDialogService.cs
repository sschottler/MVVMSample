using Microsoft.Win32;
using MVVMSample.Infrastructure.Interfaces;

namespace MVVMSample.Infrastructure.Services
{
    public class SaveFileDialogService : ISaveFileDialogService
    {
        public string Save(string initialDirectory, string filter)
        {
            return Save(initialDirectory, filter, string.Empty);
        }

        public string Save(string initialDirectory, string filter, string fileName)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            if (!string.IsNullOrEmpty(initialDirectory))
                dialog.InitialDirectory = initialDirectory;

            if (!string.IsNullOrEmpty(filter))
                dialog.Filter = filter;

            dialog.FileName = fileName;

            bool? result = dialog.ShowDialog();

            if (result == true)
                return dialog.FileName;

            return string.Empty;
        }
    }
}