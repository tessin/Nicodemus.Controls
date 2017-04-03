using Microsoft.LightSwitch.Presentation.Implementation;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Nicodemus.Controls.Checkboxes
{
    public partial class WarningCheckBox
    {
        public static readonly DependencyProperty IsWarningProperty = DependencyProperty.Register("IsWarning", typeof(bool), typeof(WarningCheckBox), new PropertyMetadata(false));

        public bool IsWarning
        {
            get { return (bool)GetValue(IsWarningProperty); }
            set { SetValue(IsWarningProperty, value); }
        }

        private Func<bool> _displayWarningFunction;

        public Func<bool> DisplayWarning
        {
            get { return _displayWarningFunction; }
            set
            {
                if (_displayWarningFunction == value) return;

                _displayWarningFunction = value;

                SetIsWarningFromDisplayWarning();
            }
        }

        public override CheckBox CheckBox { get { return checkBox; } }

        public WarningCheckBox()
        {
            InitializeComponent();
        }

        private void SetIsWarningFromDisplayWarning()
        {
            if (DisplayWarning != null)
                IsWarning = DisplayWarning();
        }

        protected override void OnContentItemSubscribed(ContentItem contentItem)
        {
            base.OnContentItemSubscribed(contentItem);
            SetIsWarningFromDisplayWarning();
        }

        protected override void OnContentItemValueChanged(ContentItem contentItem, object value)
        {
            base.OnContentItemValueChanged(contentItem, value);
            SetIsWarningFromDisplayWarning();
        }

        protected override void Checked(object sender, RoutedEventArgs e)
        {
            base.Checked(sender, e);
            SetIsWarningFromDisplayWarning();
        }

        protected override void Indeterminate(object sender, RoutedEventArgs e)
        {
            base.Indeterminate(sender, e);
            SetIsWarningFromDisplayWarning();
        }

        protected override void Unchecked(object sender, RoutedEventArgs e)
        {
            base.Unchecked(sender, e);
            SetIsWarningFromDisplayWarning();
        }
    }
}
