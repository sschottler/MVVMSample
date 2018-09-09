using MVVMSample.Infrastructure;
using MVVMSample.Infrastructure.Interfaces;

namespace MVVMSample.ViewModels.Wizards.Product
{
    public class OptionalBoatFieldsViewModel : ViewModelBase
    {
        private bool _hasJoystick;
        private bool _hasDTS;
        private int? _topSpeed;

        public bool HasJoystick
        {
            get { return _hasJoystick; }
            set
            {
                _hasJoystick = value;
                RaisePropertyChanged(() => HasJoystick);
            }
        }

        public bool HasDTS
        {
            get { return _hasDTS; }
            set
            {
                _hasDTS = value;
                RaisePropertyChanged(() => HasDTS);
            }
        }

        public int? TopSpeed
        {
            get { return _topSpeed; }
            set
            {
                _topSpeed = value;
                RaisePropertyChanged(() => TopSpeed);
            }
        }

        public OptionalBoatFieldsViewModel(ILocalizationService localizationService) : base(localizationService) {}
    }
}