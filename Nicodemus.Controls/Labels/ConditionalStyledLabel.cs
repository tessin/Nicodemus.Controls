using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Microsoft.LightSwitch.Presentation.Implementation;

namespace Nicodemus.Controls
{
    public class ConditionalStyledLabel : SelectableLabel
    {
        private Func<object, bool> _onEvaluate;
        private bool _currentValue = false;

        private Brush _originalForeground;
        //private FontFamily _originalFontFamily; // FontFamily doesn't look original when re-applied, so remove it for now
        private double _originalFontSize;
        private FontWeight _originalFontWeight;
        private FontStretch _originalFontStretch;
        private FontStyle _originalFontStyle;

        public static readonly DependencyProperty DataItemProperty = DependencyProperty.Register(
            "DataItem", typeof(object), typeof(ConditionalStyledLabel), new PropertyMetadata(OnDataItemChanged));

        public static readonly DependencyProperty ConditionalForegroundProperty = DependencyProperty.Register(
            "ConditionalForeground", typeof(Brush), typeof(ConditionalStyledLabel), new PropertyMetadata(OnChanged));

        // FontFamily doesn't look original when re-applied, so remove it for now
        //public static readonly DependencyProperty ConditionalFontFamilyProperty = DependencyProperty.Register(
        //    "ConditionalFontFamily", typeof(FontFamily), typeof(ConditionalStyledLabel), new PropertyMetadata(OnChanged));

        public static readonly DependencyProperty ConditionalFontSizeProperty = DependencyProperty.Register(
            "ConditionalFontSize", typeof(double), typeof(ConditionalStyledLabel), new PropertyMetadata(OnChanged));

        public static readonly DependencyProperty ConditionalFontWeightProperty = DependencyProperty.Register(
            "ConditionalFontWeight", typeof(FontWeight), typeof(ConditionalStyledLabel), new PropertyMetadata(OnChanged));

        public static readonly DependencyProperty ConditionalFontStretchProperty = DependencyProperty.Register(
            "ConditionalFontStretch", typeof(FontStretch), typeof(ConditionalStyledLabel), new PropertyMetadata(OnChanged));

        public static readonly DependencyProperty ConditionalFontStyleProperty = DependencyProperty.Register(
            "ConditionalFontStyle", typeof(FontStyle), typeof(ConditionalStyledLabel), new PropertyMetadata(OnChanged));

        public Brush ConditionalForeground
        {
            get { return (Brush)GetValue(ConditionalForegroundProperty); }
            set { SetValue(ConditionalForegroundProperty, value); }
        }

        // FontFamily doesn't look original when re-applied, so remove it for now
        //public FontFamily ConditionalFontFamily
        //{
        //    get { return (FontFamily)GetValue(ConditionalFontFamilyProperty); }
        //    set { SetValue(ConditionalFontFamilyProperty, value); }
        //}

        public double ConditionalFontSize
        {
            get { return (double)GetValue(ConditionalFontSizeProperty); }
            set { SetValue(ConditionalFontSizeProperty, value); }
        }

        public FontWeight ConditionalFontWeight
        {
            get { return (FontWeight)GetValue(ConditionalFontWeightProperty); }
            set { SetValue(ConditionalFontWeightProperty, value); }
        }

        public FontStretch ConditionalFontStretch
        {
            get { return (FontStretch)GetValue(ConditionalFontStretchProperty); }
            set { SetValue(ConditionalFontStretchProperty, value); }
        }

        public FontStyle ConditionalFontStyle
        {
            get { return (FontStyle)GetValue(ConditionalFontStyleProperty); }
            set { SetValue(ConditionalFontStyleProperty, value); }
        }

        public object DataItem
        {
            get { return GetValue(DataItemProperty); }
            set { SetValue(DataItemProperty, value); }
        }

        public Func<object, bool> OnEvaluate
        {
            get { return _onEvaluate; }
            set
            {
                if (_onEvaluate == value) return;
                _onEvaluate = value;
                SetForeground();
            }
        }

        public ConditionalStyledLabel()
        {
            IsReadOnly = true;
            DefaultStyleKey = typeof(SelectableLabel);
            Loaded += (s, e) =>
            {
                SetBinding(TextProperty, new Binding("Value") { Mode = BindingMode.OneWay });
                var root = ((ContentItemFromDefinition)DataContext).DataSourceRoot;
                SetBinding(DataItemProperty,
                    new Binding(root.RootBindingPath) { Source = root.RootObject, Mode = BindingMode.OneWay });
            };
            ConditionalForeground = Foreground;
            //ConditionalFontFamily = FontFamily; // FontFamily doesn't look original when re-applied, so remove it for now
            ConditionalFontSize = FontSize;
            ConditionalFontWeight = FontWeight;
            ConditionalFontStretch = FontStretch;
            ConditionalFontStyle = FontStyle;
        }

        private static void OnDataItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var instance = (ConditionalStyledLabel)sender;

            var oldDataItem = e.OldValue as INotifyPropertyChanged;
            if (oldDataItem != null)
            {
                oldDataItem.PropertyChanged -= instance.DataItemOnPropertyChanged;
            }

            var newDataItem = e.NewValue as INotifyPropertyChanged;
            if (newDataItem != null)
            {
                newDataItem.PropertyChanged += instance.DataItemOnPropertyChanged;
            }

            instance.SetForeground();
        }

        private void DataItemOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SetForeground();
        }

        private static void OnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((ConditionalStyledLabel)sender).SetForeground();
        }

        private void SetForeground()
        {
            var newValue = DataItem != null && (_onEvaluate?.Invoke(DataItem) ?? false);

            if (!_currentValue)
            {
                _originalForeground = Foreground;
                //_originalFontFamily = FontFamily; // FontFamily doesn't look original when re-applied, so remove it for now
                _originalFontSize = FontSize;
                _originalFontWeight = FontWeight;
                _originalFontStretch = FontStretch;
                _originalFontStyle = FontStyle;
            }

            Foreground = newValue ? ConditionalForeground : _originalForeground;
            //FontFamily = newValue ? ConditionalFontFamily : _originalFontFamily; // FontFamily doesn't look original when re-applied, so remove it for now
            FontSize = newValue ? ConditionalFontSize : _originalFontSize;
            FontWeight = newValue ? ConditionalFontWeight : _originalFontWeight;
            FontStretch = newValue ? ConditionalFontStretch : _originalFontStretch;
            FontStyle = newValue ? ConditionalFontStyle : _originalFontStyle;

            _currentValue = newValue;
        }
    }
}
