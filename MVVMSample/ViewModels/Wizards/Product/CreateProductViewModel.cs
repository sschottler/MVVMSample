using MVVMSample.Infrastructure.Interfaces;
using MVVMSample.Model.Interfaces;

namespace MVVMSample.ViewModels.Wizards.Product
{
    public class CreateProductViewModel : WorkspaceViewModel
    {
        public override string DisplayName
        {
            get { return GetLocalizedString("CreateProduct"); }
        }

        public ProductWizardViewModel ProductWizardViewModel
        {
            get; set;
        }

        public CreateProductViewModel(IProductRepository productRepository, ILocalizationService localizationService)
            : base(localizationService)
        {
            ProductWizardViewModel = new ProductWizardViewModel(productRepository, localizationService);

            //Close this workspace whenever the wizard finish or cancel buttons are clicked:
            ProductWizardViewModel.Cancelled += (s, args) => OnRequestClose();
            ProductWizardViewModel.Finished += (s, args) => OnRequestClose();
        }
    }
}