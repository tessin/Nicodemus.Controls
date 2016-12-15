using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
using Nicodemus.Controls;

namespace LightSwitchApplication
{
    public partial class TestDataItemsListDetail
    {
        private BusyIndicator _busyIndicator;
        private SelectFile _fileSelector;

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
            this.FindControl("MyConditionalStyledLabel").ControlAvailable += (s, e) =>
            {
                ((ConditionalStyledLabel)e.Control).ConditionalForeground = new SolidColorBrush(Colors.Red);
                ((ConditionalStyledLabel)e.Control).ConditionalFontWeight = FontWeights.Bold;
                ((ConditionalStyledLabel)e.Control).OnEvaluate = obj => ((TestDataItem)obj).BooleanField;
            };
        }

        partial void TestDataItemsListDetail_InitializeDataWorkspace(List<IDataService> saveChangesTo)
        {
            var selectFileControl = this.FindControl("SelectFile");
            selectFileControl.ControlAvailable += SelectFileControl_ControlAvailable;
        }

        private void SelectFileControl_ControlAvailable(object sender, ControlAvailableEventArgs e)
        {
            _fileSelector = e.Control as SelectFile;
            if (_fileSelector != null)
            {
                _fileSelector.TextChanged += _fileSelector_TextChanged;
            }
        }

        private void _fileSelector_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var fileName = Path.GetFileName(_fileSelector.FileName);
            var stream = _fileSelector.FileStream;
            var sha1 = GetSha1(stream);
            this.ShowMessageBox($"Name:   '{fileName}'\n" +
                                $"Length: '{stream.Length}'\n" +
                                $"SHA1:   '{sha1}'");
        }

        private static string GetSha1(Stream stream)
        {
            using (var sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(stream);
                var sb = new StringBuilder();
                foreach (var b in hash)
                {
                    sb.AppendFormat("{0:X2}", b);
                }
                return sb.ToString();
            }
        }

        partial void LongRunningMethod_Execute()
        {
            _busyIndicator.IsBusy = true;
            Thread.Sleep(10000);
            _busyIndicator.IsBusy = false;
        }

    }
}