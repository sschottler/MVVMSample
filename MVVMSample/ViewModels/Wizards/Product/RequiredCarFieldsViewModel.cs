using MVVMSample.Infrastructure.Interfaces;

namespace MVVMSample.ViewModels.Wizards.Product
{
    /// <summary>
    /// Gets rendered in the contentpresenter on the Required Product Fields Step
    /// </summary>
    public class RequiredCarFieldsViewModel : RequiredFieldsViewModel
    {
        private string _make;
        private string _model;
        private int? _year;
        private string _color;

        public string Make
        {
            get { return _make; }
            set
            {
                _make = value;
                RaisePropertyChanged(() => Make);
            }
        }

        public string Model
        {
            get { return _model; }
            set
            {
                _model = value;
                RaisePropertyChanged(() => Model);
            }
        }

        public int? Year
        {
            get { return _year; }
            set
            {
                _year = value;
                RaisePropertyChanged(() => Year);
            }
        }

        public string Color
        {
            get { return _color; }
            set
            {
                _color = value;
                RaisePropertyChanged(() => Color);
            }
        }

        public RequiredCarFieldsViewModel(ILocalizationService localizationService)
            : base(localizationService) {}

        /// <summary>
        /// Enable the next button?
        /// </summary>
        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(Color) || !Year.HasValue)
                return false;

            return true;
        }
    }
}