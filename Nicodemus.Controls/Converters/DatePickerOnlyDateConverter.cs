using System;
using System.Globalization;
using System.Windows.Data;

namespace Nicodemus.Controls.Converters
{
    // http://stackoverflow.com/a/4072813/860913
    public class DatePickerOnlyDateConverter : IValueConverter
    {
        private DateTime _original;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            _original = (DateTime)value;
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            return new DateTime(date.Year, date.Month, date.Day, _original.Hour, _original.Minute, _original.Second);
        }
    }
}
