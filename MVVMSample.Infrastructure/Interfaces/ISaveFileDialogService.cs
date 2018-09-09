namespace MVVMSample.Infrastructure.Interfaces
{
    public interface ISaveFileDialogService
    {
        string Save(string initialDirectory, string filter);
        string Save(string initialDirectory, string filter, string fileName);
    }
}