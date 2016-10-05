using System;
using Microsoft.LightSwitch.Presentation.Extensions;
using System.Windows.Controls;
using Nicodemus.Controls;

namespace LightSwitchApplication
{
    public partial class TestDataItemsListDetail
    {
        partial void TestDataItemsListDetail_Activated()
        {
            this.FindControl("TextField1").SetBinding(
                TextBox.TextProperty,
                "Value", System.Windows.Data.BindingMode.OneWay);

            this.FindControl("DateTimeField").SetBinding(
                UtcDateTimePicker.ValueProperty,
                "Value", System.Windows.Data.BindingMode.TwoWay);

            this.FindControl("DateTimeField").ControlAvailable += (s, e) =>
            {
                ((UtcDateTimePicker)e.Control).TimeZoneOffset = TimeSpan.FromHours(2);
            };
        }
    }
}