using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using com.bodurov.SilverlightControls.XmlCodeEditor;

namespace Nicodemus.Controls
{
    public class XmlEditorTest : XmlCodeEditorBox
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(XmlEditorTest), new PropertyMetadata(OnTextChanged));

        public XmlEditorTest()
        {
            Loaded += (s, e) => SetBinding(TextProperty, new Binding("Value") { Mode = BindingMode.OneWay });
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((XmlEditorTest)d).Text = Regex.Replace((string)e.NewValue, "\r([^\n])", "\r\n$1");
        }
    }
}
