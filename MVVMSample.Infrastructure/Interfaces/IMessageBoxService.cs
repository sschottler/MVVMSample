using System.Windows.Forms;

namespace MVVMSample.Infrastructure.Interfaces
{
    /// <summary>
    /// Encapsulate details of opening messageboxes so this service can be mocked or changed
    /// </summary>
    public interface IMessageBoxService
    {
        DialogResult ShowMessageBox(string title, string message, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon);
    }
}