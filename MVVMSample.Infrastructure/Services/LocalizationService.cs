using System;
using System.Globalization;
using MVVMSample.Infrastructure.Interfaces;
using WPFLocalizeExtension.Engine;
using WPFLocalizeExtension.Extensions;

namespace MVVMSample.Infrastructure.Services
{
    public class LocalizationService : ILocalizationService
    {
        //better to set this in the app startup, but this is just a sample:
        private string _resxDefaultAssembly = "MVVMSample";
        private string _resxDefaultDictionary = "Strings";

        public event EventHandler<CultureChangedEventArgs> CultureChanged;

        public string ResxDefaultAssembly
        {
            get { return _resxDefaultAssembly; }
            set { _resxDefaultAssembly = value; }
        }

        public string ResxDefaultDictionary
        {
            get { return _resxDefaultDictionary; }
            set { _resxDefaultDictionary = value; }
        }

        public CultureInfo Culture
        {
            get { return LocalizeDictionary.Instance.Culture; }
            set
            {
                if (value != null && !value.Equals(LocalizeDictionary.Instance.Culture))
                {
                    CultureInfo oldCulture = LocalizeDictionary.Instance.Culture;
                    LocalizeDictionary.Instance.Culture = value;

                    if (CultureChanged != null)
                        CultureChanged(this, new CultureChangedEventArgs(oldCulture, value));
                }
            }
        }

        public string GetLocalizedString(string key)
        {
            return GetLocalizedString(key, ResxDefaultAssembly, ResxDefaultDictionary);
        }

        public string GetLocalizedString(string key, string assembly, string dictionary)
        {
            string value;

            string prefix = string.Format("{0}:{1}:", assembly, dictionary);

            LocExtension locExtension = new LocExtension(prefix + key);
            locExtension.ResolveLocalizedValue(out value);

            return value;
        }
    }
}