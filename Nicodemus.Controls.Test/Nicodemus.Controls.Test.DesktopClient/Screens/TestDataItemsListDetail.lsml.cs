using System;
using Microsoft.LightSwitch.Presentation.Extensions;
using Nicodemus.Controls;

namespace LightSwitchApplication
{
    public partial class TestDataItemsListDetail
    {
        partial void TestDataItemsListDetail_Activated()
        {
            this.FindControl("DateTimeField").ControlAvailable += (s, e) =>
            {
                ((UtcDateTimePicker)e.Control).TimeZoneOffset = TimeSpan.FromHours(2);
            };
            this.FindControl("DateTimeField1").ControlAvailable += (s, e) =>
            {
                ((UtcDateTimeLabel)e.Control).TimeZoneOffset = TimeSpan.FromHours(2);
                ((UtcDateTimeLabel)e.Control).Format = "g";
            };
        }
    }
}