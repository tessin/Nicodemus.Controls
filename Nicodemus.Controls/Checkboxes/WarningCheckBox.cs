using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Nicodemus.Controls.Checkboxes
{
    public class WarningCheckBox : CheckBox
    {
        private Func<bool> _displayWarningFunction;

        public static readonly DependencyProperty IsWarningProperty = DependencyProperty.Register(
            "IsWarning", typeof(bool), typeof(WarningCheckBox), new PropertyMetadata(false));

        public bool IsWarning
        {
            get { return (bool)GetValue(IsWarningProperty); }
            set { SetValue(IsWarningProperty, value); }
        }

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

        public WarningCheckBox()
        {
            DefaultStyleKey = typeof(WarningCheckBox);

            Checked += (s, e) => SetIsWarningFromDisplayWarning();
            Unchecked += (s, e) => SetIsWarningFromDisplayWarning();
            Indeterminate += (s, e) => SetIsWarningFromDisplayWarning();

            Loaded += (s, e) => SetBinding(IsCheckedProperty, new Binding("Value") { Mode = BindingMode.OneWay });
        }

        private void SetIsWarningFromDisplayWarning()
        {
            if (DisplayWarning != null)
                IsWarning = DisplayWarning();
        }
    }
}
