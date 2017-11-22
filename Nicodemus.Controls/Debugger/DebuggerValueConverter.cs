using System;
using System.Globalization;
using System.Windows.Data;

namespace Nicodemus.Controls.Debugger
{
    public class DebuggerValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var typeCode = Type.GetTypeCode(value?.GetType());

            if (value == null)
            {
                return $"[{typeCode}]";
            }
            else
            {
                return $"[{typeCode} {System.Convert.ToString(value, culture)}]";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("this converter does not support two way data binding");
        }
    }
}
