using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using System.Windows.Input;
using MVVMSample.Infrastructure;
using MVVMSample.Infrastructure.Interfaces;

namespace MVVMSample.ViewModels
{
    public enum Result
    {
        Ok, Cancel    
    }

    public class LanguageSelectionViewModel : ViewModelBase
    {
        private CultureInfo _selectedCulture;
        private Result _result = Result.Cancel;
        private ReadOnlyCollection<CultureInfo> _availableCultures;
 
        public ICommand OKCommand
        {
            get; private set;
        }

        public ICommand CancelCommand
        {
            get; private set;
        }

        public Result Result
        {
            get { return _result; }
            set { _result = value; }
        }

        public CultureInfo SelectedCulture
        {
            get { return _selectedCulture; }
            set
            {
                _selectedCulture = value;
                RaisePropertyChanged(() => SelectedCulture);
            }
        }

        public ReadOnlyCollection<CultureInfo> AvailableCultures
        {
            get
            {
                if (_availableCultures == null)
                {
                    _availableCultures = new ReadOnlyCollection<CultureInfo>(
                        new List<CultureInfo>
                        {
                            CultureInfo.GetCultureInfo("en-US"),
                            CultureInfo.GetCultureInfo("de"),
                            CultureInfo.GetCultureInfo("zh")
                        });
                }

                return _availableCultures;
            }
        }

        public LanguageSelectionViewModel(ILocalizationService localizationService) : base(localizationService)
        {
            SelectedCulture = Thread.CurrentThread.CurrentCulture;

            OKCommand = new RelayCommand(OK);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void OK()
        {
            Result = Result.Ok;
            OnRequestClose();
        }

        private void Cancel()
        {
            Result = Result.Cancel;
            OnRequestClose();
        }
    }
}