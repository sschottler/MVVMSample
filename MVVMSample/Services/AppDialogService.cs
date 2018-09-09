using System;
using System.Globalization;
using System.Windows;
using MVVMSample.Infrastructure.Interfaces;
using MVVMSample.Interfaces;
using MVVMSample.ViewModels;

namespace MVVMSample.Services
{
    /// <summary>
    /// Encapsulates the view construction details away from the ViewModels so they aren't tightly-coupled to specific Views
    /// </summary>
    public class AppDialogService : IAppDialogService
    {
        private IDialogService _dialogService;
        private ILocalizationService _localizationService;
        //factory methods that create our views - dependency injection containers allow you to register instances as shared or non-shared,
        //but since we aren't using a DI container, we just pass in factory methods in the constructor that return an IView so we can mock
        //for testing without having to be coupled to the UI layer:
        private Func<IView> _languageSelectionViewFactory;
        private Func<IView> _aboutViewFactory;
 
        /// <summary>
        /// You could have several dialog services (example: CustomerDialogService, ProductDialogService, etc.)
        /// Some people will call these "Controllers," but when people use the term "Controller," they're often
        /// referring to a class with many more responsiblities than simply opening views.
        /// We inject a generic IDialogService helper class that handles creating the containing window, modal vs non modal, 
        /// sizing window to usercontrol content, positioning, etc. The reason we don't reference the DialogService directly
        /// in the ViewModels and have to go through this appdialogservice is because the dialogservice has methods 
        /// with UserControl parameters, so the caller needs to know the view type, and we don't want the viewmodels
        /// to be tightly-coupled to specific views, but we also don't want to rewrite these helper methods to open
        /// windows in every single class we write, so we inject that as a dependency and delegate to it
        /// </summary>
        public AppDialogService(IDialogService dialogService, ILocalizationService localizationService, Func<IView> languageSelectionViewFactory, Func<IView> aboutViewFactory)
        {
            _aboutViewFactory = aboutViewFactory;
            _languageSelectionViewFactory = languageSelectionViewFactory;
            _dialogService = dialogService;
            _localizationService = localizationService;
        }

        /// <summary>
        /// Called from ViewModels, who should be completely-agnostic of View types.
        /// </summary>
        /// <returns>Null if cancelled, otherwise the selected culture</returns>
        public CultureInfo ShowLanguageSelectionDialog()
        {
            LanguageSelectionViewModel viewModel = new LanguageSelectionViewModel(_localizationService);
            //get our view from the factory method:
            IView view = _languageSelectionViewFactory();

            //can grab other windows from Application.Current.Windows collection if you have child of a child...
            //this is a UI dependency - should be factored out for testability like the Views were:
            Window parentWindow = Application.Current.MainWindow;
            string windowTitle = _localizationService.GetLocalizedString("LanguageSelection");

            _dialogService.ShowDialog(view, viewModel, windowTitle, true, parentWindow);

            //could probably have a DialogViewModelBase, could also use eventaggregator/messenger/mediator to throw event from the child window
            if (viewModel.Result == Result.Ok) 
                return viewModel.SelectedCulture;

            return null;
        }

        public void ShowAboutDialog()
        {
            //get view from the factory method passed in:
            IView view = _aboutViewFactory();

            string title = _localizationService.GetLocalizedString("About");
            Window parentWindow = Application.Current.MainWindow;

            _dialogService.ShowDialog(view, null, title, false, parentWindow);
        }
    }
}