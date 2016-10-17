using System.Windows.Controls;
using System.Windows.Data;

namespace Nicodemus.Controls
{
    public class LinkButton : HyperlinkButton
    {
        public LinkButton()
        {
            Loaded += (s, e) => SetBinding(ContentProperty, new Binding("Value") { Mode = BindingMode.OneWay });
        }
    }
}
