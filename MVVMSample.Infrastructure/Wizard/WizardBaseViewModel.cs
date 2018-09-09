using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using MVVMSample.Infrastructure.Interfaces;

namespace MVVMSample.Infrastructure.Wizard
{
    /// <summary>
    /// Reusable Wizard base ViewModel that handles common navigation, progress operations, etc.
    /// </summary>
    public abstract class WizardBaseViewModel : ViewModelBase
    {
        private string _nextLabel;
        private bool _isBusy;
        private string _busyMessage;
        private WizardNavigationEventArgs _currentNavigationArgs;
        private WizardStepBaseViewModel _currentStep;
        private ObservableCollection<WizardStepBaseViewModel> _steps = new ObservableCollection<WizardStepBaseViewModel>();
        
        public event EventHandler Cancelled;
        public event EventHandler Finished;

        public string NextLabel
        {
            get { return _nextLabel; }
            set
            {
                _nextLabel = value;
                RaisePropertyChanged(() => NextLabel);
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public string BusyMessage
        {
            get { return _busyMessage; }
            set
            {
                _busyMessage = value;
                RaisePropertyChanged(() => BusyMessage);
            }
        }

        public ICommand CancelCommand
        {
            get; private set;
        }

        public ICommand StartOverCommand
        {
            get; private set;
        }

        public ICommand BackCommand
        {
            get; private set;
        }

        public ICommand NextCommand
        {
            get; private set;
        }

        public WizardStepBaseViewModel CurrentStep
        {
            get
            {
                if (_currentStep == null && Steps.Count > 0)
                    _currentStep = Steps[0];

                return _currentStep;
            }
            set
            {
                if (_currentStep != null)
                    _currentStep.IsCurrentStep = false;

                _currentStep = value;

                if (_currentStep != null)
                    _currentStep.IsCurrentStep = true;

                RaisePropertyChanged(() => CurrentStep);
                RaisePropertyChanged(() => CurrentStepIndex);
                RaisePropertyChanged(() => IsOnLastStep);

                SetNextLabel();
            }
        }

        public int CurrentStepIndex
        {
            get { return Steps.IndexOf(CurrentStep); }
        }

        public bool IsOnLastStep
        {
            get { return Steps.Count > 0 && (CurrentStepIndex == Steps.Count - 1); }
        }

        public ObservableCollection<WizardStepBaseViewModel> Steps
        {
            get { return _steps; }
        }

        protected WizardBaseViewModel(ILocalizationService localizationService) : base(localizationService)
        {
            StartOverCommand = new RelayCommand(StartOver);
            BackCommand = new RelayCommand(MoveBack, CanMoveBack);
            NextCommand = new RelayCommand(MoveNext, CanMoveNext);
            CancelCommand = new RelayCommand(Cancel);

            if (localizationService != null)
                BusyMessage = LocalizationService.GetLocalizedString("PleaseWait");
            else
                BusyMessage = "Please Wait...";

            Steps.CollectionChanged += WizardStepsChanged;

            SetNextLabel();
        }

        private void WizardStepsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (WizardStepBaseViewModel step in e.NewItems)
                    step.Wizard = this;

            if (e.OldItems != null)
                foreach (WizardStepBaseViewModel step in e.OldItems)
                    step.Wizard = null;
        }

        protected virtual void Cancel()
        {
            if (Cancelled != null)
                Cancelled(this, EventArgs.Empty);
        }

        protected virtual void StartOver()
        {
            foreach (WizardStepBaseViewModel step in Steps)
                step.Clear();

            CurrentStep = Steps[0];
        }

        /// <summary>
        /// Disable the Back button?
        /// </summary>
        protected virtual bool CanMoveBack()
        {
            return CurrentStep.AllowBackNavigation && 0 < CurrentStepIndex;
        }

        protected virtual void MoveBack()
        {
            if (CanMoveBack())
                Navigate(WizardNavigationType.Back);
        }

        /// <summary>
        /// Disable the Next button?
        /// </summary>
        protected virtual bool CanMoveNext()
        {
            return CurrentStep != null && CurrentStep.IsValid();
        }

        protected virtual void MoveNext()
        {
            if (CanMoveNext())
            {
                if (CurrentStepIndex < Steps.Count - 1)
                    Navigate(WizardNavigationType.Next);
                else
                {
                    //if last step, raise the Finished event (could kick off actual operation or could just close the wizard
                    //if another step already kicked off the operation and the last step is just a summary step)
                    if (Finished != null)
                        Finished(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Called during Back or Next navigation
        /// </summary>
        private void Navigate(WizardNavigationType navigationType)
        {
            WizardStepBaseViewModel newStep = null;

            if (navigationType == WizardNavigationType.Back)
                newStep = Steps[CurrentStepIndex - 1];
            else if (navigationType == WizardNavigationType.Next)
                newStep = Steps[CurrentStepIndex + 1];

            _currentNavigationArgs = new WizardNavigationEventArgs(navigationType, CurrentStep, newStep);

            //Gives derived Wizard ViewModel a chance to pause or cancel navigation and also coordinate data interaction
            //between steps if necessary:
            OnNavigating(_currentNavigationArgs);

            if (_currentNavigationArgs.NavigationAction == WizardNavigationAction.Continue)
                ContinueNavigation();
            else if (_currentNavigationArgs.NavigationAction == WizardNavigationAction.Cancel)
                CancelNavigation();
        }

        /// <summary>
        /// Does nothing by default, but derived Wizard ViewModel can override and use this to pause
        /// or cancel navigation and coordinate data between steps
        /// </summary>
        /// <param name="eventArgs"></param>
        protected virtual void OnNavigating(WizardNavigationEventArgs eventArgs)
        {
        }

        public void CancelNavigation()
        {
            _currentNavigationArgs = null;
        }

        public void ContinueNavigation()
        {
            if (_currentNavigationArgs != null)
            {
                CurrentStep = _currentStep = _currentNavigationArgs.NewStep;
                _currentNavigationArgs = null;
            }
        }

        /// <summary>
        /// Just a helper method to get a specific step from the collection
        /// </summary>
        public T GetStep<T>()
        {
            return Steps.OfType<T>().FirstOrDefault();
        }

        protected override void OnCultureChanged(CultureChangedEventArgs args)
        {
            //refresh the next/finish button text:
            SetNextLabel();
        }

        private void SetNextLabel()
        {
            if (LocalizationService != null)
            {
                NextLabel = IsOnLastStep
                    ? LocalizationService.GetLocalizedString("Finish")
                    : LocalizationService.GetLocalizedString("Next");
            }
            else
            {
                NextLabel = IsOnLastStep ? "Finish" : "Next >";
            }
        }
    }
}