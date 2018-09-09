using System;
using System.Globalization;

namespace MVVMSample.Infrastructure.Interfaces
{
    public class CultureChangedEventArgs : EventArgs
    {
        public CultureInfo OldCulture { get; private set; }
        public CultureInfo NewCulture { get; private set; }

        public CultureChangedEventArgs(CultureInfo oldCulture, CultureInfo newCulture)
        {
            OldCulture = oldCulture;
            NewCulture = newCulture;
        }
    }

    public interface ILocalizationService
    {
        CultureInfo Culture { get; set; }

        event EventHandler<CultureChangedEventArgs> CultureChanged;

        string GetLocalizedString(string key);
        //this is sort of specific to RESX Provider. If this wrapped a database provider (eg SqlLite storage) this method wouldn't make sense, but this is just a sample:
        string GetLocalizedString(string key, string assembly, string dictionary);
    }
}