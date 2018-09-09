using System;

namespace MVVMSample.Infrastructure.Wizard
{
    public class WizardNavigationEventArgs : EventArgs
    {
        private WizardNavigationAction _navigationAction = WizardNavigationAction.Continue;

        public WizardNavigationType NavigationType { get; private set; }

        /// <summary>
        /// Set this to Pause in OnNavigating override in your derived WizardViewModel if you want
        /// to show a confirmation or progress dialog while you perform an asynchronous operation.
        /// You can also set it to cancel if user said "No" to confirmation dialog box or something.
        /// If you set to Pause, you must call ContinueNavigation or CancelNavigation on the WizardBaseViewModel
        /// base class at some point from your derived Wizard ViewModel
        /// </summary>
        public WizardNavigationAction NavigationAction
        {
            get { return _navigationAction; }
            set { _navigationAction = value; }
        }

        public WizardStepBaseViewModel CurrentStep { get; private set; }
        public WizardStepBaseViewModel NewStep { get; private set; }

        public WizardNavigationEventArgs(WizardNavigationType navigationType, WizardStepBaseViewModel currentStep, WizardStepBaseViewModel newStep)
        {
            NavigationType = navigationType;
            CurrentStep = currentStep;
            NewStep = newStep;
        }
    }
}