using System;
using System.Globalization;
using System.Windows;

namespace Nicodemus.Controls.Labels
{
    public class BooleanToVisibleIfTrueConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var nullableValue = value as bool?;

            if (nullableValue == true)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("this converter does not support two way data binding");
        }
    }
}
