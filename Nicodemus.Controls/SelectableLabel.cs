using System.Windows.Controls;
using System.Windows.Data;

namespace Nicodemus.Controls
{
    public class SelectableLabel : TextBox
    {
        public SelectableLabel()
        {
            IsReadOnly = true;
            DefaultStyleKey = typeof(SelectableLabel);
            Loaded += (s, e) => SetBinding(TextProperty, new Binding("Value") { Mode = BindingMode.OneWay });
        }
    }
}
