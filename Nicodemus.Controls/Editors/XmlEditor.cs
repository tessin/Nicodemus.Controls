using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using com.bodurov.SilverlightControls.XmlCodeEditor;

namespace Nicodemus.Controls
{
    public class XmlEditor : XmlCodeEditorBox
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(XmlEditor), new PropertyMetadata(OnTextChanged));

        public XmlEditor()
        {
            Loaded += (s, e) => SetBinding(TextProperty, new Binding("Value") { Mode = BindingMode.OneWay });
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((XmlEditor)d).Text = Regex.Replace((string)e.NewValue, "\r([^\n])", "\r\n$1");
        }
    }
}
