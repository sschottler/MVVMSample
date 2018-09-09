using System.Windows.Input;
using MVVMSample.Infrastructure;
using MVVMSample.Infrastructure.Interfaces;

namespace MVVMSample.ViewModels
{
    public abstract class WorkspaceViewModel : ViewModelBase
    {
        private ICommand _closeCommand;

        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                    _closeCommand = new RelayCommand(OnRequestClose);

                return _closeCommand;
            }
        }

        public abstract string DisplayName { get; }

        public WorkspaceViewModel(ILocalizationService localizationService) : base(localizationService) {}

        protected override void OnCultureChanged(CultureChangedEventArgs args)
        {
            //refresh displayname when culture changes (derived classes have to implement DisplayName property and will pull it from LocalizationService):
            RaisePropertyChanged(() => DisplayName);
        }
    }
}