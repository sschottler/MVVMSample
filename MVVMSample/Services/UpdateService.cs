using System;
using System.Threading;
using System.Threading.Tasks;
using MVVMSample.Interfaces;

namespace MVVMSample.Services
{
    /// <summary>
    /// Update service called from tools -> Update. Simulates long-running operation with progress updates
    /// </summary>
    public class UpdateService : IUpdateService
    {
        public void Update(Action<Progress> progressCallback)
        {
            //simulate updates:
            Progress progress = new Progress
            {
                Percentage = 0,
                Message = ProgressMessage.CheckingForUpdates
            };

            //invoke the callback passed in with current progress:
            progressCallback(progress);

            //simulate async operation:
            Task.Factory.StartNew(() =>
            {            
                Thread.Sleep(TimeSpan.FromSeconds(1));

                progress.Percentage = 33;
                progress.Message = ProgressMessage.DownloadingUpdates;

                //report to UI callback:
                progressCallback(progress);

                Thread.Sleep(TimeSpan.FromSeconds(2));

                progress.Percentage = 67;
                progress.Message = ProgressMessage.ApplyingUpdates;

                //report to UI callback:
                progressCallback(progress);

                Thread.Sleep(TimeSpan.FromSeconds(2));

                progress.Percentage = 100;
                progress.Message = ProgressMessage.UpdatesSuccessful;

                //report to UI callback:
                progressCallback(progress);
            });
        }
    }
}