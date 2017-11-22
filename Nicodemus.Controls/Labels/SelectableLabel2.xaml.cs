using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Nicodemus.Controls
{
    public partial class SelectableLabel2 : UserControl
    {
        public SelectableLabel2()
        {
            InitializeComponent();
        }

        private void CopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(textBox.Text);
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            copyToClipboardButton.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            copyToClipboardButton.Visibility = Visibility.Collapsed;
        }

        private void contentControl_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!Convert.ToBoolean(e.NewValue))
            {
                // LS actually disables the entire control tree further up, which in turn disables interactivity
                // the only way around this problem is to walk the visual tree upwards 
                // looking for props that influence the control tree and remove them
                
                // total hours wasted here == 4

                Dispatcher.BeginInvoke(() =>
                {
                    var c = VisualTreeHelper.GetParent(contentControl);
                    while (c != null)
                    {
                        if (c is Control)
                        {
                            var hasValue = c.ReadLocalValue(Control.IsEnabledProperty) != DependencyProperty.UnsetValue;
                            if (hasValue)
                            {
                                var isEnabled = ((Control)c).IsEnabled;
                                if (!isEnabled)
                                {
                                    //var oldValue = c.ReadLocalValue(Control.IsEnabledProperty);

                                    c.ClearValue(Control.IsEnabledProperty); // this can actually be a data binding

                                    //var newValue = c.ReadLocalValue(Control.IsEnabledProperty);

                                    if (contentControl.IsEnabled)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        c = VisualTreeHelper.GetParent(c);
                    }
                });
            }
        }
    }
}
