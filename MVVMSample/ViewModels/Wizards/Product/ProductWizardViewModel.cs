using MVVMSample.Infrastructure.Interfaces;
using MVVMSample.Infrastructure.Wizard;
using MVVMSample.Model;
using MVVMSample.Model.Interfaces;
using Products = MVVMSample.Model.Products;

namespace MVVMSample.ViewModels.Wizards.Product
{
    /// <summary>
    /// Parent Wizard that handles adding the steps and coordinating interactions between them
    /// </summary>
    public class ProductWizardViewModel : WizardBaseViewModel
    {
        //inject this dependency:
        private IProductRepository _productRepository;

        /// Simplify access to specific steps with properties that use the GetStep<T> helper method:

        public ProductTypeStepViewModel ProductTypeStepViewModel
        {
            get { return GetStep<ProductTypeStepViewModel>(); }
        }

        public ProductRequiredStepViewModel ProductRequiredStepViewModel
        {
            get { return GetStep<ProductRequiredStepViewModel>(); }
        }

        public ProductOptionalStepViewModel ProductOptionalStepViewModel
        {
            get { return GetStep<ProductOptionalStepViewModel>(); }
        }

        public SummaryStepViewModel SummaryStepViewModel
        {
            get { return GetStep<SummaryStepViewModel>(); }
        }

        public ProductWizardViewModel(IProductRepository productRepository, ILocalizationService localizationService) : base(localizationService)
        {
            _productRepository = productRepository;

            Steps.Add(new ProductTypeStepViewModel(localizationService));
            Steps.Add(new ProductRequiredStepViewModel(localizationService));
            Steps.Add(new ProductOptionalStepViewModel(localizationService));
            //SummaryStepViewModel is shared between customer and product wizards:
            Steps.Add(new SummaryStepViewModel(localizationService));
        }

        /// <summary>
        /// This method is called on back or next click and allows the parent wizard to show 
        /// dialogs/progress messages and configure the next step based on the current step's properties
        /// </summary>
        /// <param name="eventArgs">Navigation EventArgs with info on Navigation Direction, Action, and current and new steps</param>
        protected override void OnNavigating(WizardNavigationEventArgs eventArgs)
        {
            if (eventArgs.NavigationType == WizardNavigationType.Next)
            {
                //figure out what step we're moving to:
                if (eventArgs.NewStep is ProductRequiredStepViewModel)
                    SetRequiredFieldsViewModel();
                else if (eventArgs.NewStep is ProductOptionalStepViewModel)
                    SetOptionalFieldsViewModel();
                else if (eventArgs.NewStep is SummaryStepViewModel)
                {
                    //pause could be used to show a confirmation dialog or show a busymessage to run an async operation
                    //you must call ContinueNavigation or CancelNavigation after calling pause!
                    eventArgs.NavigationAction = WizardNavigationAction.Pause;

                    CreateProduct();
                }                
            }
        }

        /// <summary>
        /// Set RequiredFieldsViewModel type based on selected product type
        /// </summary>
        private void SetRequiredFieldsViewModel()
        {
            if (ProductTypeStepViewModel.SelectedProductType == typeof (Products.Boat) &&
                //second check is necessary so we don't always reset and clear user's previous textbox values
                //(They could've clicked back, made a change without modifying product type, and then clicked Next again)
                !(ProductRequiredStepViewModel.RequiredFieldsViewModel is RequiredBoatFieldsViewModel))
            {
                ProductRequiredStepViewModel.RequiredFieldsViewModel = new RequiredBoatFieldsViewModel(LocalizationService);                
            }
            else if (ProductTypeStepViewModel.SelectedProductType == typeof (Products.Car) &&
                     !(ProductRequiredStepViewModel.RequiredFieldsViewModel is RequiredCarFieldsViewModel))
            {
                ProductRequiredStepViewModel.RequiredFieldsViewModel = new RequiredCarFieldsViewModel(LocalizationService);                
            }
        }

        /// <summary>
        /// Set OptionalFieldsViewModel based on selected product type (exact same pattern as required step)
        /// </summary>
        private void SetOptionalFieldsViewModel()
        {
            if (ProductTypeStepViewModel.SelectedProductType == typeof(Products.Boat) &&
                !(ProductOptionalStepViewModel.OptionalFieldsViewModel is OptionalBoatFieldsViewModel))
            {
                ProductOptionalStepViewModel.OptionalFieldsViewModel = new OptionalBoatFieldsViewModel(LocalizationService);
            }
            else if (ProductTypeStepViewModel.SelectedProductType == typeof(Products.Car) &&
                     !(ProductOptionalStepViewModel.OptionalFieldsViewModel is OptionalCarFieldsViewModel))
            {
                ProductOptionalStepViewModel.OptionalFieldsViewModel = new OptionalCarFieldsViewModel(LocalizationService);
            }
        }

        private void CreateProduct()
        {
            //tell the UI to show that an operation is running:
            IsBusy = true;
            BusyMessage = LocalizationService.GetLocalizedString("CreatingProduct");

            Products.Product product = null;

            if (ProductTypeStepViewModel.SelectedProductType == typeof(Products.Car))
                product = ConstructCar();
            else if (ProductTypeStepViewModel.SelectedProductType == typeof(Products.Boat))
                product = ConstructBoat();

            //repository simulates long running async process and takes a callback:
            _productRepository.CreateProduct(product, ProductCreatedCallback);
        }

        /// <summary>
        /// Build the Car object from the step viewmodel properties if selected product type was car
        /// </summary>
        private Products.Car ConstructCar()
        {
            RequiredCarFieldsViewModel requiredFields = (RequiredCarFieldsViewModel)ProductRequiredStepViewModel.RequiredFieldsViewModel;
            OptionalCarFieldsViewModel optionalFields = (OptionalCarFieldsViewModel)ProductOptionalStepViewModel.OptionalFieldsViewModel;

            return new Products.Car(ProductTypeStepViewModel.Name, requiredFields.Make, requiredFields.Model,
                requiredFields.Year.Value, requiredFields.Color)
            {
                Description = ProductTypeStepViewModel.Description,
                HasGPS = optionalFields.HasGPS,
                HasSunroof = optionalFields.HasSunroof
            };
        }

        /// <summary>
        /// Build the Product object from the step viewmodel properties if selected product type was Boat
        /// </summary>
        private Products.Boat ConstructBoat()
        {
            RequiredBoatFieldsViewModel requiredFields = (RequiredBoatFieldsViewModel)ProductRequiredStepViewModel.RequiredFieldsViewModel;
            OptionalBoatFieldsViewModel optionalFields =
                (OptionalBoatFieldsViewModel) ProductOptionalStepViewModel.OptionalFieldsViewModel;

            return new Products.Boat(ProductTypeStepViewModel.Name, requiredFields.EngineType, requiredFields.Length.Value)
            {
                Description = ProductTypeStepViewModel.Description,
                HasDTS = optionalFields.HasDTS,
                HasJoystick = optionalFields.HasJoystick
            };
        }

        /// <summary>
        /// Invoked from ProductRepository when async process of creation completes
        /// </summary>
        /// <param name="response"></param>
        private void ProductCreatedCallback(ResponseCode response)
        {
            //tell UI the operation finished:
            IsBusy = false;
            //set the response on the summary step:
            SummaryStepViewModel.Response = response;
            //we are currently paused so we can show the progress in the UI. Call this to continue on to the summary step:
            ContinueNavigation();
        }
    }
}