using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using Microsoft.LightSwitch.Presentation.Utilities.Internal;
using Nicodemus.Controls.Helpers;

namespace Nicodemus.Controls
{
    public class UtcDateTimePicker : Control
    {

        public static readonly DependencyProperty TimeZoneOffsetProperty = DependencyProperty.Register(
            "TimeZoneOffset", typeof(TimeSpan?), typeof(UtcDateTimePicker), new PropertyMetadata(OnTimeZoneOffsetChanged));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value",
            typeof(DateTime?), typeof(UtcDateTimePicker), new PropertyMetadata(OnValueChanged));

        public static readonly DependencyProperty LocalValueProperty = DependencyProperty.Register("LocalValue",
            typeof(DateTime?), typeof(UtcDateTimePicker), new PropertyMetadata(OnLocalValueChanged));

        public static TimeSpan DefaultTimeZoneOffset { get; set; }

        public TimeSpan? TimeZoneOffset
        {
            get { return (TimeSpan?)(GetValue(TimeZoneOffsetProperty)) ?? DefaultTimeZoneOffset; }
            set { SetValue(TimeZoneOffsetProperty, value); }
        }

        public DateTime? Value
        {
            get { return (DateTime?)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public DateTime? LocalValue
        {
            get { return (DateTime?)GetValue(LocalValueProperty); }
            set { SetValue(LocalValueProperty, value); }
        }

        static UtcDateTimePicker()
        {
            DefaultTimeZoneOffset = DateTime.Now.UtcOffset();
        }

        public UtcDateTimePicker()
        {
            DefaultStyleKey = typeof(UtcDateTimePicker);
            Loaded += (s, e) =>
            {
                SetBinding(ValueProperty, new Binding("Value") {Mode = BindingMode.TwoWay});

                var datePicker = this.FindVisualChildByType<DatePicker>();
                datePicker.CalendarOpened += DatePicker_CalendarOpened;
            };
        }

        private static void OnTimeZoneOffsetChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (UtcDateTimePicker)sender;
            var time = control.Value;
            control.LocalValue = time?.SumWithoutOverflow(control.TimeZoneOffset ?? TimeSpan.Zero);
        }

        private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (UtcDateTimePicker)sender;
            var time = control.Value;
            control.LocalValue = time?.SumWithoutOverflow(control.TimeZoneOffset ?? TimeSpan.Zero);
        }

        private static void OnLocalValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (UtcDateTimePicker)sender;
            var time = control.LocalValue;
            if (!time.HasValue) return;
            control.Value = time.Value.SumWithoutOverflow(-control.TimeZoneOffset ?? TimeSpan.Zero);
        }

        private void DatePicker_CalendarOpened(object sender, RoutedEventArgs e)
        {
            Value = DateTime.UtcNow;
        }
    }
}
