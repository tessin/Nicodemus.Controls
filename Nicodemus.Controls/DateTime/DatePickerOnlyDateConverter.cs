using System;
using System.Globalization;
using System.Windows.Data;

namespace Nicodemus.Controls.Common
{
    // http://stackoverflow.com/a/4072813/860913
    public class DatePickerOnlyDateConverter : IValueConverter
    {
        private DateTime? _original;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            _original = (DateTime?)value;
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime?)value;
            
            return date.HasValue && _original.HasValue ? 
                new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 
                _original.Value.Hour, _original.Value.Minute, _original.Value.Second) : DateTime.UtcNow;
        }
    }
}
