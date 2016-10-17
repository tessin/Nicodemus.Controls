using System;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Nicodemus.Controls.Helpers;

namespace Nicodemus.Controls
{
    public class UtcDateTimeLabel : SelectableLabel
    {
        public static readonly DependencyProperty TimeZoneOffsetProperty = DependencyProperty.Register(
            "TimeZoneOffset", typeof(TimeSpan), typeof(UtcDateTimeLabel), new PropertyMetadata(OnTimeZoneOffsetChanged));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value",
            typeof(DateTime), typeof(UtcDateTimeLabel), new PropertyMetadata(OnValueChanged));

        public static readonly DependencyProperty FormatProperty = DependencyProperty.Register("Format",
            typeof(string), typeof(UtcDateTimeLabel), new PropertyMetadata(OnFormatChanged));

        public TimeSpan TimeZoneOffset
        {
            get { return (TimeSpan)GetValue(TimeZoneOffsetProperty); }
            set { SetValue(TimeZoneOffsetProperty, value); }
        }

        public DateTime Value
        {
            get { return (DateTime)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public string Format
        {
            get { return (string)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }

        public UtcDateTimeLabel()
        {
            DefaultStyleKey = typeof(UtcDateTimeLabel);
            Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.Name);
            Loaded += (s, e) => SetBinding(ValueProperty, new Binding("Value") { Mode = BindingMode.OneWay });
        }

        private static void OnTimeZoneOffsetChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((UtcDateTimeLabel)sender).SetText();
        }

        private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((UtcDateTimeLabel)sender).SetText();
        }

        private static void OnFormatChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((UtcDateTimeLabel)sender).SetText();
        }

        private void SetText()
        {
            var localValue = Value.SumWithoutOverflow(TimeZoneOffset);
            Text = localValue.ToString(!string.IsNullOrWhiteSpace(Format) ? Format : "g");
        }
    }
}
