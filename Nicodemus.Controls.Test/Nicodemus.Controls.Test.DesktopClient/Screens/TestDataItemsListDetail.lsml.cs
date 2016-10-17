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
            this.FindControl("DateTimeField").ControlAvailable += (s, e) =>
            {
                ((UtcDateTimePicker)e.Control).TimeZoneOffset = TimeSpan.FromHours(2);
            };
        }
    }
}