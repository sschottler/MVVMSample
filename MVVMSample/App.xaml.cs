using System.Windows;
using MVVMSample.Infrastructure;
using MVVMSample.Infrastructure.Interfaces;
using MVVMSample.Infrastructure.Services;
using MVVMSample.Interfaces;
using MVVMSample.Model.Interfaces;
using MVVMSample.Model.Repositories;
using MVVMSample.Services;
using MVVMSample.Views;
using MVVMSample.ViewModels;

namespace MVVMSample
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
        	base.OnStartup(e);

            MainWindowView view = new MainWindowView();

            //inject dependencies (could use a dependency injection framework for this, but not strictly necessary):
            ICustomerRepository customerRepository = new CustomerRepository();
            IProductRepository productRepository = new ProductRepository();
            ILocalizationService localizationService = new LocalizationService();
            IUpdateService updateService = new UpdateService();
            IMessageBoxService messageBoxService = new MessageBoxService();
            IDialogService dialogService = new DialogService();
            IAppDialogService appDialogService = new AppDialogService(dialogService, localizationService, ()=> new LanguageSelectionView(), ()=> new AboutView());
            IOpenFileDialogService openFileDialogService = new OpenFileDialogService();
            ISaveFileDialogService saveFileDialogService = new SaveFileDialogService();

            MainWindowViewModel viewModel = new MainWindowViewModel(
                customerRepository,
                productRepository,
                updateService,
                messageBoxService,
                appDialogService,
                openFileDialogService,
                saveFileDialogService,
                localizationService);

            //could also be wired up and handled in MainWindow.xaml.cs code behind (maybe in DataContextChanged override for example)
            //also could have a controller class listening to this event
            viewModel.RequestClose += (s, args) => view.Close();

            view.DataContext = viewModel;
            view.Show();
        }
    }
}