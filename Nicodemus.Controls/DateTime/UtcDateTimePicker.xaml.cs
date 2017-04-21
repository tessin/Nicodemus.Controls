using Microsoft.LightSwitch.Presentation.Implementation;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Nicodemus.Controls
{
    public partial class UtcDateTimePicker
    {
        public DateTime DefaultDate { get; set; } = DateTime.Now.Date;
        public TimeSpan DefaultTimeOfDay { get; set; } = new TimeSpan(12, 00, 00);

        //

        public static readonly DependencyProperty TimeZoneProperty = DependencyProperty.Register("TimeZone",
            typeof(TimeZoneInfo), typeof(UtcDateTimePicker), new PropertyMetadata(TimeZoneInfo.Local, OnTimeZoneChanged));

        public TimeZoneInfo TimeZone
        {
            get { return (TimeZoneInfo)GetValue(TimeZoneProperty); }
            set { SetValue(TimeZoneProperty, value); }
        }

        private static void OnTimeZoneChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        private DateTime ConvertLocalToUtc(DateTime localDateTime)
        {
            var utcOffset = TimeZone.GetUtcOffset(localDateTime);
            return localDateTime - utcOffset;
        }

        private DateTime ConvertUtcToLocal(DateTime utcDateTime)
        {
            var utcOffset = TimeZone.GetUtcOffset(utcDateTime);
            return utcDateTime + utcOffset;
        }

        //

        private readonly EventHandler<SelectionChangedEventArgs> _selectedDateChanged;
        private readonly RoutedPropertyChangedEventHandler<DateTime?> _timePickerValueChanged;

        Guid id_;

        public UtcDateTimePicker()
        {
            InitializeComponent();

            timePicker.Format = new CustomTimeFormat("HH:mm");

#if DEBUG
            id_ = Guid.NewGuid();
#endif
            _selectedDateChanged = new EventHandler<SelectionChangedEventArgs>(SelectedDateChanged);
            _timePickerValueChanged = new RoutedPropertyChangedEventHandler<DateTime?>(TimePickerValueChanged);
        }

        private void SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            PushLocalAsUtc();
        }

        private void TimePickerValueChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            PushLocalAsUtc();
        }

        protected override void OnLoaded()
        {
            System.Diagnostics.Debug.WriteLine($"{id_}: Loaded");
        }

        protected override void OnContentItemSubscribed(ContentItem contentItem)
        {
            System.Diagnostics.Debug.WriteLine($"{id_}: ContentItemSubscribed");

            // init
            var value = contentItem.Value as DateTime?;
            SetLocalFromUtc(value);

            datePicker.SelectedDateChanged += _selectedDateChanged;
            timePicker.ValueChanged += _timePickerValueChanged;
        }

        protected override void OnContentItemValueChanged(ContentItem contentItem, object value)
        {
            System.Diagnostics.Debug.WriteLine($"{id_}: ContentItemValueChanged Value={value}");

            SetLocalFromUtc(value as DateTime?);
        }

        protected override void OnContentItemUnsubscribed(ContentItem contentItem)
        {
            System.Diagnostics.Debug.WriteLine($"{id_}: ContentItemUnsubscribed");

            timePicker.ValueChanged += _timePickerValueChanged;
            datePicker.SelectedDateChanged += _selectedDateChanged;

            SetLocalFromUtc(null);
        }

        protected override void OnUnloaded()
        {
            System.Diagnostics.Debug.WriteLine($"{id_}: Unloaded");
        }

        private void SetLocalFromUtc(DateTime? value)
        {
            if (value == null)
            {
                datePicker.SelectedDate = null;
                timePicker.Value = null;
            }
            else
            {
                var value2 = ConvertUtcToLocal(value.Value);

                datePicker.SelectedDate = value2;
                timePicker.Value = value2;
            }
        }

        private void PushLocalAsUtc()
        {
            if (ContentItem != null)
            {
                if (datePicker.SelectedDate == null)
                {
                    var currentValue = ContentItem.Value as DateTime?;
                    if (currentValue != null)
                    {
                        ContentItem.Value = null;
                    }
                    return;
                }

                // local date & time
                var date = datePicker.SelectedDate.Value.Date;
                var time = timePicker.Value?.TimeOfDay ?? DefaultTimeOfDay;

                var localDateAndTime = date + time;

                // UTC offset
                var utcOffset = TimeZone.GetUtcOffset(localDateAndTime);

                // UTC date & time
                var utcDateAndTime = localDateAndTime - utcOffset;

                var currentValue2 = ContentItem.Value as DateTime?;
                if (currentValue2 != utcDateAndTime)
                {
                    ContentItem.Value = utcDateAndTime;
                }
            }
        }
    }
}
