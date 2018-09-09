using System.Windows.Forms;
using MVVMSample.Infrastructure.Interfaces;

namespace MVVMSample.Infrastructure.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        public DialogResult ShowMessageBox(string title, string message, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon)
        {
            return MessageBox.Show(message, title, messageBoxButtons, messageBoxIcon);
        }
    }
}