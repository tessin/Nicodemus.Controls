﻿using System;
using System.Threading;
using System.Windows;
using Microsoft.LightSwitch.Presentation.Extensions;
using Nicodemus.Controls;

namespace LightSwitchApplication
{
    public partial class TestDataItemsListDetail
    {
        private BusyIndicator _busyIndicator;

        partial void TestDataItemsListDetail_Activated()
        {
            _busyIndicator = new BusyIndicator(this);

            this.FindControl("DateTimeField").ControlAvailable += (s, e) =>
            {
                ((UtcDateTimePicker)e.Control).TimeZoneOffset = TimeSpan.FromHours(2);
            };
            this.FindControl("DateTimeField1").ControlAvailable += (s, e) =>
            {
                ((UtcDateTimeLabel)e.Control).TimeZoneOffset = TimeSpan.FromHours(2);
                ((UtcDateTimeLabel)e.Control).Format = "g";
            };
            this.FindControl("LinkTextField").ControlAvailable += (s, e) =>
            {
                ((LinkButton)e.Control).Click += (sender, args) =>
                {
                    MessageBox.Show(((LinkButton)sender).Content.ToString());
                };
            };

        }

        partial void LongRunningMethod_Execute()
        {
            _busyIndicator.IsBusy = true;
            Thread.Sleep(10000);
            _busyIndicator.IsBusy = false;
        }
    }
}