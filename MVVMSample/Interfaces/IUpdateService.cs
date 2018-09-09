using System;

namespace MVVMSample.Interfaces
{
    public enum ProgressMessage
    {
        CheckingForUpdates,
        DownloadingUpdates,
        ApplyingUpdates,
        UpdatesSuccessful,
        UpdatesFailed
    }

    public class Progress
    {
        public decimal Percentage { get; set; }
        public ProgressMessage Message { get; set; }
    }

    /// <summary>
    /// Interface for Update Service that takes a callback for reporting progress percentage and messages
    /// </summary>
    public interface IUpdateService
    {
        void Update(Action<Progress> progressCallback);
    }
}