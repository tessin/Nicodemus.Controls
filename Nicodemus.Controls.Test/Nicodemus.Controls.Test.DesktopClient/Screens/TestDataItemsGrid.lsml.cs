using System;
using Microsoft.LightSwitch.Presentation.Extensions;
using System.Windows.Controls;
using Microsoft.LightSwitch.Presentation.Utilities.Internal;
using Nicodemus.Controls;

namespace LightSwitchApplication
{
    public partial class TestDataItemsGrid
    {
        partial void TestDataItemsGrid_Activated()
        {
            this.FindControl("grid").ControlAvailable += (_, e) =>
            {
                var grid = (DataGrid)e.Control;
                grid.Columns[2].IsReadOnly = true;
                grid.LoadingRow += (__, e2) =>
                {
                    e2.Row.Loaded += (___, _____) =>
                    {
                        e2.Row.VisitVisualChildren<UtcDateTimeLabel>(label =>
                        {
                            label.TimeZoneOffset = TimeSpan.FromHours(2);
                            label.Format = "g";
                        });
                    };
                };
            };
        }
    }
}