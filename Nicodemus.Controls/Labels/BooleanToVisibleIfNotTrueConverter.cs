using System;
using System.Globalization;
using System.Windows;

namespace Nicodemus.Controls.Labels
{
    public class BooleanToVisibleIfNotTrueConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var nullableValue = value as bool?;

            if (nullableValue == true)
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("this converter does not support two way data binding");
        }
    }
}
