﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using LightSwitchApplication.Models;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Presentation.Extensions;
using Nicodemus.Controls;
using Nicodemus.Controls.Busy;
using Nicodemus.Controls.Checkboxes;
using Nicodemus.Controls.Dialogs;
using Nicodemus.Controls.Editors;

namespace LightSwitchApplication
{
    public partial class TestDataItemsListDetail
    {
        private SelectFile _fileSelector;

        partial void TestDataItemsListDetail_Activated()
        {

            

            //UtcDateTimePicker.DefaultTimeZoneOffset = TimeSpan.Zero;
            UtcDateTimeLabel.DefaultCulture = new CultureInfo("sv-SE");

            //this.FindControl("DateTimeLabel").ControlAvailable += (s, e) =>
            //{
            //    ((UtcDateTimeLabel)e.Control).TimeZoneOffset = TimeSpan.FromHours(5);
            //    ((UtcDateTimeLabel)e.Control).Format = "g";
            //};

            this.FindControl("LinkTextField").ControlAvailable += (s, e) =>
            {
                ((LinkButton)e.Control).Click += (sender, args) =>
                {
                    MessageBox.Show(((LinkButton)sender).Content.ToString());
                };
            };

            this.FindControl("ComputedTextField").ControlAvailable += (s, e) =>
            {
                ((LinkButton)e.Control).Click += (sender, args) =>
                {
                    MessageBox.Show(((LinkButton)sender).Content.ToString());
                };
            };

            //this.FindControl("MyConditionalStyledLabel").ControlAvailable += (s, e) =>
            //{
            //    ((ConditionalStyledLabel)e.Control).ConditionalForeground = new SolidColorBrush(Colors.Red);
            //    ((ConditionalStyledLabel)e.Control).ConditionalFontWeight = FontWeights.Bold;
            //    ((ConditionalStyledLabel)e.Control).OnEvaluate = obj => ((TestDataItem)obj).BooleanField;
            //};

            this.FindControl("WarningBooleanField").ControlAvailable += (s, e) =>
            {
                ((WarningCheckBox)e.Control).DisplayWarning = () => ((WarningCheckBox)e.Control).IsChecked ?? false;
            };

            this.FindControl("XmlField2").ControlAvailable += (sender, e) =>
            {
                ((XmlTextEditor)e.Control).IsXmlValidationEnabled = true;
                ((XmlTextEditor)e.Control).XmlValidationError += (err) =>
                {
                    this.ShowMessageBox(err.Message, "XML Error", MessageBoxOption.Ok);
                };
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
            var control = sender as IContentItemProxy;
            if (control != null && _fileSelector != null)
            {
                if (control.DisplayName == "Select File") _fileSelector.Filter = "pdf document (*.pdf)|*.pdf";
                _fileSelector.TextChanged += _fileSelector_TextChanged;
            }
        }

        private void _fileSelector_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var fileName = Path.GetFileName(_fileSelector.File.Name);
            var stream = _fileSelector.File.Stream;
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
            var waiter = new BusyWaiter(60000);
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(10000);
                waiter.Cancel();
            });
            waiter.Start();
        }

        partial void ShowPropertyWindow_Execute()
        {
            new PropertyWindowBuilder().Show(new PropertyWindowDemoViewModel(), "Title of Window");
        }

        partial void ShowBusyWindow_Execute()
        {
            WaitWindow.Show();
        }
    }
}