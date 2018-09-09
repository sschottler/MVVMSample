using MVVMSample.Infrastructure;
using MVVMSample.Infrastructure.Interfaces;

namespace MVVMSample.ViewModels.Wizards.Product
{
    public class OptionalCarFieldsViewModel : ViewModelBase
    {
        private bool _hasGPS;
        private bool _hasSunroof;

        public bool HasGPS
        {
            get { return _hasGPS; }
            set
            {
                _hasGPS = value;
                RaisePropertyChanged(() => HasGPS);
            }
        }

        public bool HasSunroof
        {
            get { return _hasSunroof; }
            set
            {
                _hasSunroof = value;
                RaisePropertyChanged(() => HasSunroof);
            }
        }

        public OptionalCarFieldsViewModel(ILocalizationService localizationService) : base(localizationService) { }
    }
}