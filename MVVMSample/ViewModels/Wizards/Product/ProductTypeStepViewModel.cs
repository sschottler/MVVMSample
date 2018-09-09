using System;
using System.Collections.Generic;
using MVVMSample.Infrastructure.Interfaces;
using MVVMSample.Infrastructure.Wizard;
using MVVMSample.Model.Products;

namespace MVVMSample.ViewModels.Wizards.Product
{
    public class ProductTypeStepViewModel : WizardStepBaseViewModel
    {
        private string _name;
        private string _description;
        private Type _selectedProductType = typeof (Boat);
        private List<Type> _productTypes;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        public Type SelectedProductType
        {
            get { return _selectedProductType; }
            set
            {
                _selectedProductType = value;
                RaisePropertyChanged(() => SelectedProductType);
            }
        }

        public List<Type> ProductTypes
        {
            get
            {
                if (_productTypes == null)
                    _productTypes = new List<Type> {typeof (Car), typeof (Boat)};

                return _productTypes;
            }
        }

        public ProductTypeStepViewModel(ILocalizationService localizationService) : base(localizationService)
        {
            SetDisplayName();
        }

        /// <summary>
        /// Should we enable the Next button?
        /// </summary>
        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(Name);
        }

        /// <summary>
        /// Called on Start Over
        /// </summary>
        public override void Clear()
        {
            Name = Description = string.Empty;
            SelectedProductType = typeof (Boat);
        }

        protected override void OnCultureChanged(CultureChangedEventArgs args)
        {
            //refresh displayname when culture changes:
            SetDisplayName();
        }

        private void SetDisplayName()
        {
            DisplayName = LocalizationService.GetLocalizedString("ProductType");
        }
    }
}