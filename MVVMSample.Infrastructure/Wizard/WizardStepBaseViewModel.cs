using MVVMSample.Infrastructure.Interfaces;

namespace MVVMSample.Infrastructure.Wizard
{
    public abstract class WizardStepBaseViewModel : ViewModelBase
    {
        private bool _isCurrentStep;
        private string _displayName;
        private bool _allowStartOver = true;
        private bool _allowBackNavigation = true;
        private bool _allowNextNavigation = true;
        private bool _allowCancel = true;

        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                RaisePropertyChanged(() => DisplayName);
            }
        }

        public bool IsCurrentStep
        {
            get { return _isCurrentStep; }
            set
            {
                _isCurrentStep = value;
                RaisePropertyChanged(() => IsCurrentStep);
            }
        }

        /// <summary>
        /// Disable the Start Over button from this step
        /// </summary>
        public bool AllowStartOver
        {
            get { return _allowStartOver; }
            set
            {
                _allowStartOver = value;
                RaisePropertyChanged(() => AllowStartOver);
            }
        }

        /// <summary>
        /// Disable the Back button from this step
        /// </summary>
        public bool AllowBackNavigation
        {
            get { return _allowBackNavigation; }
            set
            {
                _allowBackNavigation = value;
                RaisePropertyChanged(() => AllowBackNavigation);
            }
        }

        /// <summary>
        /// Disable the Next button from this step
        /// </summary>
        public bool AllowNextNavigation
        {
            get { return _allowNextNavigation; }
            set
            {
                _allowNextNavigation = value;
                RaisePropertyChanged(() => AllowNextNavigation);
            }
        }

        /// <summary>
        /// Disable the Cancel button from this step
        /// </summary>
        public bool AllowCancel
        {
            get { return _allowCancel; }
            set
            {
                _allowCancel = value;
                RaisePropertyChanged(() => AllowCancel);
            }
        }

        public WizardBaseViewModel Wizard
        {
            get; set;
        }

        protected WizardStepBaseViewModel(ILocalizationService localizationService)
            : base(localizationService) {}

        /// <summary>
        /// Override to add logic to disable next button
        /// </summary>
        /// <returns></returns>
        public virtual bool IsValid()
        {
            return true;
        }

        /// <summary>
        /// Override to clear properties when Start Over button is clicked
        /// </summary>
        public virtual void Clear()
        {
        }
    }
}