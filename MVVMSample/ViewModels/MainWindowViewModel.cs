using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Threading;
using System.Windows.Input;
using MVVMSample.Infrastructure;
using MVVMSample.Infrastructure.Interfaces;
using MVVMSample.Interfaces;
using MVVMSample.Model.Interfaces;
using MVVMSample.ViewModels.Wizards.Customer;
using MVVMSample.ViewModels.Wizards.Product;

namespace MVVMSample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private decimal _progressPercentage;
        private bool _isBusy;
        private ProgressMessage _progressMessage;
        private WorkspaceViewModel _selectedWorkspace;
        private ObservableCollection<WorkspaceViewModel> _workspaces;
        private ReadOnlyCollection<CommandViewModel> _commands;

        //dependencies to inject (don't want to be tightly-coupled to the behavior or these classes,
        //so we program to their interface and inject them in the constructor):
        private ICustomerRepository _customerRepository;
        private IProductRepository _productRepository;
        private IUpdateService _updateService;
        private IMessageBoxService _messageBoxService;
        private IAppDialogService _appDialogService;
        private IOpenFileDialogService _openFileDialogService;
        private ISaveFileDialogService _saveFileDialogService;

        /// <summary>
        /// Property for the UI to show that an operation is being performed
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        /// <summary>
        /// Enum representing message key to translate for reporting progress of a long-running operation
        /// </summary>
        public ProgressMessage ProgressMessage
        {
            get { return _progressMessage; }
            set
            {
                _progressMessage = value;
                RaisePropertyChanged(() => ProgressMessage);
            }
        }

        /// <summary>
        /// Bound to ProgressBar to show progress
        /// </summary>
        public decimal ProgressPercentage
        {
            get { return _progressPercentage; }
            set
            {
                _progressPercentage = value;
                RaisePropertyChanged(() => ProgressPercentage);
            }
        }

        public ICommand OpenFileCommand
        {
            get; private set;
        }

        public ICommand SaveFileCommand
        {
            get; private set;
        }

        public ICommand CloseCommand
        {
            get; private set;
        }

        public ICommand OpenAboutCommand
        {
            get; private set;
        }

        public ICommand OpenLanguageOptionsCommand
        {
            get; private set;
        }

        public ICommand UpdateCommand
        {
            get; private set;
        }

        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_commands == null)
                    _commands = new ReadOnlyCollection<CommandViewModel>(CreateCommands());

                return _commands;
            }
        }

        private List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(GetLocalizedString("CreateCustomer"), new RelayCommand(OpenCustomerWizard), LocalizationService),
                new CommandViewModel(GetLocalizedString("CreateProduct"), new RelayCommand(OpenProductWizard), LocalizationService)
            };
        }

        /// <summary>
        /// Based on the way it's implemented in the UI, this ends up being rendered as the selected tab
        /// </summary>
        public WorkspaceViewModel SelectedWorkspace
        {
            get { return _selectedWorkspace; }
            set
            {
                _selectedWorkspace = value;
                RaisePropertyChanged(() => SelectedWorkspace);
            }
        }

        /// <summary>
        /// Based on the way it's rendered in the UI, each workspace is rendered as a separate tab
        /// </summary>
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_workspaces == null)
                {
                    _workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _workspaces.CollectionChanged += OnWorkspacesChanged;
                }

                return _workspaces;
            }
        }

        /// <summary>
        /// Wire up eventhandlers for the OnRequestClose events of the Workspace ViewModels
        /// OnRequestClose is raised by the workspace when the x button in the tab is closed
        /// or when the wizard is cancelled or finished
        /// </summary>
        private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += OnWorkspaceRequestClose;

            if (e.OldItems != null)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= OnWorkspaceRequestClose;
        }

        /// <summary>
        /// Remove the workspace from the databound collection when the RequestClose event is raised
        /// </summary>
        private void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            workspace.Dispose();
            Workspaces.Remove(workspace);
        }

        /// <summary>
        /// Constructor using dependency injection
        /// </summary>
        public MainWindowViewModel(
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IUpdateService updateService,
            IMessageBoxService messageBoxService,
            IAppDialogService appDialogService,
            IOpenFileDialogService openFileDialogService, 
            ISaveFileDialogService saveFileDialogService,
            ILocalizationService localizationService) : base(localizationService)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;

            _updateService = updateService;
            _messageBoxService = messageBoxService;
            _appDialogService = appDialogService;
            _openFileDialogService = openFileDialogService;
            _saveFileDialogService = saveFileDialogService;

            OpenFileCommand = new RelayCommand(OpenFileDialog);
            SaveFileCommand = new RelayCommand(SaveFileDialog);
            CloseCommand = new RelayCommand(OnRequestClose);
            OpenAboutCommand = new RelayCommand(OpenAboutDialog);
            OpenLanguageOptionsCommand = new RelayCommand(OpenLanguageOptions);
            UpdateCommand = new RelayCommand(Update);
        }

        private void OpenFileDialog()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _openFileDialogService.OpenFileDialog(folderPath);
        }

        private void SaveFileDialog()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //syntax should be encapsulated in service since it is specific to the SaveFileDialog it wraps, but this is just a sample app:
            string filter = "XML Files | *.xml"; 

            _saveFileDialogService.Save(folderPath, filter);
        }

        private void OpenCustomerWizard()
        {
            //pass dependencies to CreateCustomerViewModel workspace and add it to the workspaces collection, 
            //which will result in new tab appearing in UI:
            var customerWizard = new CreateCustomerViewModel(_customerRepository, _messageBoxService, LocalizationService);
            Workspaces.Add(customerWizard);
            SelectedWorkspace = customerWizard;
        }

        private void OpenProductWizard()
        {
            //pass dependencies to CreateProductViewModel workspace and add it to the workspaces collection, 
            //which will result in new tab appearing in UI:
            var productWizard = new CreateProductViewModel(_productRepository, LocalizationService);
            Workspaces.Add(productWizard);
            SelectedWorkspace = productWizard;
        }

        private void OpenAboutDialog()
        {
            //forward call to AppDialogService (don't want to directly open views from viewmodels since that introduces tight-coupling):
            _appDialogService.ShowAboutDialog();
        }

        private void OpenLanguageOptions()
        {
            //forward call to AppDialogService (don't want to directly open views from viewmodels since that introduces tight-coupling):
            CultureInfo culture = _appDialogService.ShowLanguageSelectionDialog();
            LocalizationService.Culture = culture;
        }

        private void Update()
        {
            //tell UI we're performing an operation
            IsBusy = true;
            //call update service and pass callback to be invoked for progress reports
            _updateService.Update(UpdateProgressCallback);
        }

        /// <summary>
        /// Called when IUpdateService has progress message to report
        /// </summary>
        /// <param name="progress">Encapsulates Progress percentage and message describing current step in the operation</param>
        private void UpdateProgressCallback(Progress progress)
        {
            ProgressMessage = progress.Message;
            ProgressPercentage = progress.Percentage;

            if (progress.Message == ProgressMessage.UpdatesSuccessful)
            {
                //pause for second to show full progress bar and successful message (not ideal for production app)
                //(could also use dialogservice to show messagebox here that updates were successfully applied):
                Thread.Sleep(TimeSpan.FromSeconds(1));
                //tell UI the operation has completed
                IsBusy = false;                
            }
        }

        protected override void OnCultureChanged(CultureChangedEventArgs args)
        {
            //set commands to null so they're rebuilt with new translations when culture changes:
            _commands = null;
            RaisePropertyChanged(() => Commands);
        }
    }
}