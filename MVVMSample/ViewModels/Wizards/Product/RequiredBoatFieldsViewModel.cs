using System.Collections.Generic;
using MVVMSample.Infrastructure.Interfaces;
using MVVMSample.Model.Products;

namespace MVVMSample.ViewModels.Wizards.Product
{
    /// <summary>
    /// Gets rendered in the contentpresenter on the Required Product Fields Step
    /// </summary>
    public class RequiredBoatFieldsViewModel : RequiredFieldsViewModel
    {
        private BoatEngineType _engineType = BoatEngineType.Outboard;
        private int? _length;

        public BoatEngineType EngineType
        {
            get { return _engineType; }
            set
            {
                _engineType = value;
                RaisePropertyChanged(() => EngineType);
            }
        }

        public List<BoatEngineType> EngineTypes
        {
            get; private set;
        }

        public int? Length
        {
            get { return _length; }
            set
            {
                _length = value;
                RaisePropertyChanged(() => Length);
            }
        }

        public RequiredBoatFieldsViewModel(ILocalizationService localizationService) : base(localizationService)
        {
            EngineTypes = new List<BoatEngineType>
            {
                BoatEngineType.Outboard,
                BoatEngineType.Inboard,
                BoatEngineType.Sterndrive,
                BoatEngineType.Jet
            };
        }

        /// <summary>
        /// Enable the next button?
        /// </summary>
        public override bool IsValid()
        {
            return Length.HasValue;
        }
    }
}