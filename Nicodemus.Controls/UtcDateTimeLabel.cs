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
            "TimeZoneOffset", typeof(TimeSpan?), typeof(UtcDateTimeLabel), new PropertyMetadata(OnTimeZoneOffsetChanged));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value",
            typeof(DateTime?), typeof(UtcDateTimeLabel), new PropertyMetadata(OnValueChanged));

        public static readonly DependencyProperty FormatProperty = DependencyProperty.Register("Format",
            typeof(string), typeof(UtcDateTimeLabel), new PropertyMetadata(OnFormatChanged));

        public static TimeSpan DefaultTimeZoneOffset { get; set; }

        public static string DefaultFormat { get; set; }

        public TimeSpan? TimeZoneOffset
        {
            get
            {
                var value = GetValue(TimeZoneOffsetProperty);
                return (TimeSpan?)(value ?? DefaultTimeZoneOffset);
            }
            set { SetValue(TimeZoneOffsetProperty, value); }
        }

        public DateTime? Value
        {
            get { return (DateTime?)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public string Format
        {
            get { return (string)GetValue(FormatProperty) ?? DefaultFormat; }
            set { SetValue(FormatProperty, value); }
        }

        static UtcDateTimeLabel()
        {
            DefaultTimeZoneOffset = DateTime.Now.UtcOffset();
            DefaultFormat = "g";
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
            var local = Value?.SumWithoutOverflow(TimeZoneOffset ?? TimeSpan.Zero);
            Text = local.HasValue ? local.Value.ToString(!string.IsNullOrWhiteSpace(Format) ? 
                Format : DefaultFormat) : "";
        }

    }
}
