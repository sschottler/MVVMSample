using System;

namespace MVVMSample.Infrastructure.Interfaces
{
    /// <summary>
    /// WPF does not allow background threads to update things like databound ObservableCollection<T>'s
    /// because they are bound on the dispatcher (UI) thread. Therefore, we encapsulate 
    /// the Dispatcher thread in this interface and call it's invoke method when we need
    /// to update these collections. This will ensure that the code is executed on the
    /// dispatcher thread. This interface is injected in the ViewModel's constructor instead
    /// of directly referencing Dispatcher.CurrentDispatcher to facilitate loose-coupling of 
    /// the View & ViewModel and to allow us to inject mock implementations for unit-testing
    /// when the Dispatcher thread may be inapplicable since the ViewModel code could be 
    /// executed from a class library.
    /// </summary>
    public interface IDispatcherService
    {
        void Invoke(Action executeMethod);
    }
}