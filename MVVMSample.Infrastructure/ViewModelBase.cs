using System;
using System.Diagnostics;
using MVVMSample.Infrastructure.Interfaces;

namespace MVVMSample.Infrastructure
{
    public abstract class ViewModelBase : ObservableObject, IDisposable
    {
        private ILocalizationService _localizationService;

        protected ILocalizationService LocalizationService
        {
            get { return _localizationService; }
        }

        protected ViewModelBase(ILocalizationService localizationService)
        {
            _localizationService = localizationService;

            if (_localizationService != null)
                _localizationService.CultureChanged += LocalizationServiceCultureChanged;
        }

        public event EventHandler RequestClose;

        protected virtual void OnRequestClose()
        {
            if (RequestClose != null)
                RequestClose(this, EventArgs.Empty);

            UnsubscribeCultureChangedEvent();
        }

        #region IDisposable

        /// <summary>
        /// Invoked when this object is being removed from the application
        /// and will be subject to garbage collection.
        /// </summary>
        public void Dispose()
        {
            this.OnDispose();
        }

        /// <summary>
        /// Child classes can override this method to perform 
        /// clean-up logic, such as removing event handlers.
        /// </summary>
        protected virtual void OnDispose()
        {
            UnsubscribeCultureChangedEvent();
        }

#if DEBUG
    /// <summary>
        /// Useful for ensuring that ViewModel objects are properly garbage collected.
        /// </summary>
        ~ViewModelBase()
        {
            string msg = string.Format("{0} Finalized", this.GetType().Name);
            Debug.WriteLine(msg);
        }
#endif

        #endregion

        private void UnsubscribeCultureChangedEvent()
        {
            if (_localizationService != null)
                _localizationService.CultureChanged -= LocalizationServiceCultureChanged;
        }

        protected string GetLocalizedString(string key)
        {
            return _localizationService.GetLocalizedString(key);
        }

        protected string GetLocalizedString(string key, string assembly, string dictionary)
        {
            return _localizationService.GetLocalizedString(key, assembly, dictionary);
        }

        private void LocalizationServiceCultureChanged(object sender, CultureChangedEventArgs args)
        {
            OnCultureChanged(args);
        }

        /// <summary>
        /// Override this in derived viewmodels to raise propertychanged events for any localized properties that are databound
        /// </summary>
        protected virtual void OnCultureChanged(CultureChangedEventArgs args)
        {
        }
    }
}