using System;
using System.Globalization;

namespace Nicodemus.Controls.Labels
{
    public class BooleanToEnglishConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var nullableValue = value as bool?;

            if (nullableValue == true)
            {
                return "Yes";
            }

            if (nullableValue == false)
            {
                return "No";
            }

            return "Not set";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
