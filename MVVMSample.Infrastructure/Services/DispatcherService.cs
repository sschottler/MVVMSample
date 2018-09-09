using System;
using System.Windows.Threading;
using System.Windows;
using MVVMSample.Infrastructure.Interfaces;

namespace MVVMSample.Infrastructure.Services
{
    /// <summary>
    /// Wrap the Dispatcher to make unit testing mocking easier
    /// </summary>
    public class DispatcherInvoker : IDispatcherService
    {
        private Dispatcher dispatcher = Application.Current.Dispatcher;

        public void Invoke(Action executeMethod)
        {
            if (executeMethod == null)
                throw new ArgumentNullException("executeMethod");
           
            if (dispatcher == null || dispatcher.CheckAccess())
                executeMethod();
            else
                dispatcher.Invoke(executeMethod);
        }
    }
}