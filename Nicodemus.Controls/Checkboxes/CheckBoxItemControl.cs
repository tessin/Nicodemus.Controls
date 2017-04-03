using Microsoft.LightSwitch.Presentation.Implementation;
using Nicodemus.Controls.Common;
using System.Windows;
using System.Windows.Controls;

namespace Nicodemus.Controls.Checkboxes
{
    public abstract class CheckBoxItemControl : UserContentItemControl
    {
        public abstract CheckBox CheckBox { get; }

        public bool? IsChecked
        {
            get { return CheckBox.IsChecked; }
            set { CheckBox.IsChecked = value; }
        }

        private readonly RoutedEventHandler _checked;
        private readonly RoutedEventHandler _indeterminate;
        private readonly RoutedEventHandler _unchecked;

        public CheckBoxItemControl()
        {
            _checked = new RoutedEventHandler(Checked);
            _indeterminate = new RoutedEventHandler(Indeterminate);
            _unchecked = new RoutedEventHandler(Unchecked);
        }

        protected override void OnLoaded()
        {
        }

        protected override void OnContentItemSubscribed(ContentItem contentItem)
        {
            CheckBox.IsChecked = (bool?)contentItem.Value;

            CheckBox.Checked += _checked;
            CheckBox.Indeterminate += _indeterminate;
            CheckBox.Unchecked += _unchecked;
        }

        protected override void OnContentItemValueChanged(ContentItem contentItem, object value)
        {
            CheckBox.IsChecked = (bool?)value;
        }

        protected virtual void Checked(object sender, RoutedEventArgs e)
        {
            if (ContentItem != null)
            {
                ContentItem.Value = true;
            }
        }

        protected virtual void Indeterminate(object sender, RoutedEventArgs e)
        {
            if (ContentItem != null)
            {
                ContentItem.Value = null;
            }
        }

        protected virtual void Unchecked(object sender, RoutedEventArgs e)
        {
            if (ContentItem != null)
            {
                ContentItem.Value = false;
            }
        }

        protected override void OnContentItemUnsubscribed(ContentItem contentItem)
        {
            CheckBox.Checked -= _checked;
            CheckBox.Indeterminate -= _indeterminate;
            CheckBox.Unchecked -= _unchecked;

            CheckBox.IsChecked = null;
        }

        protected override void OnUnloaded()
        {
        }
    }
}
