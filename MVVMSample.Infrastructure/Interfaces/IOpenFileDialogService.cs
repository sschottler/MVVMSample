namespace MVVMSample.Infrastructure.Interfaces
{
    public interface IOpenFileDialogService
    {
        string OpenFileDialog(string initialDirectory);
        string OpenFileDialog(string initialDirectory, string filter);
    }
}