using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using Nicodemus.Controls.Helpers;

namespace Nicodemus.Controls
{
    public class UtcDateTimePicker : Control
    {
        public static readonly DependencyProperty TimeZoneOffsetProperty = DependencyProperty.Register(
            "TimeZoneOffset", typeof(TimeSpan), typeof(UtcDateTimePicker), new PropertyMetadata(OnTimeZoneOffsetChanged));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value",
            typeof(DateTime), typeof(UtcDateTimePicker), new PropertyMetadata(OnValueChanged));

        public static readonly DependencyProperty LocalValueProperty = DependencyProperty.Register("LocalValue",
            typeof(DateTime), typeof(UtcDateTimePicker), new PropertyMetadata(OnLocalValueChanged));

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

        public DateTime LocalValue
        {
            get { return (DateTime)GetValue(LocalValueProperty); }
            set { SetValue(LocalValueProperty, value); }
        }

        public UtcDateTimePicker()
        {
            DefaultStyleKey = typeof(UtcDateTimePicker);
            Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.Name);
            Loaded += (s, e) => SetBinding(ValueProperty, new Binding("Value") { Mode = BindingMode.TwoWay });
        }

        private static void OnTimeZoneOffsetChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (UtcDateTimePicker)sender;
            control.LocalValue = control.Value.SumWithoutOverflow(control.TimeZoneOffset);
        }

        private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (UtcDateTimePicker)sender;
            control.LocalValue = control.Value.SumWithoutOverflow(control.TimeZoneOffset);
        }

        private static void OnLocalValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (UtcDateTimePicker)sender;
            control.Value = control.LocalValue.SumWithoutOverflow(-control.TimeZoneOffset);
        }
    }
}
