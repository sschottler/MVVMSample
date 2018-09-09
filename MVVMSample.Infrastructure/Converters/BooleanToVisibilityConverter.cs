using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MVVMSample.Infrastructure.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public bool InvertInput { get; set; }
        public bool ConvertVisibilityToBoolean { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (ConvertVisibilityToBoolean)
            {
                if (value is Visibility)
                {
                    Visibility input = (Visibility)value;
                    return input == Visibility.Visible;
                }
            }
            else
            {
                if (value is bool)
                {
                    bool input = (bool)value;

                    if (InvertInput)
                        input = !input;

                    return input ? Visibility.Visible : Visibility.Collapsed;
                }

                return Visibility.Visible;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (ConvertVisibilityToBoolean)
            {
                bool visible = (bool)value;
                return visible ? Visibility.Visible : Visibility.Collapsed;
            }

            return null;
        }
    }
}