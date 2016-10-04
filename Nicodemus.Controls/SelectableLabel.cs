using System.Windows.Controls;

namespace Nicodemus.Controls
{
    public class SelectableLabel : TextBox
    {
        public SelectableLabel()
        {
            IsReadOnly = true;
            DefaultStyleKey = typeof(SelectableLabel);
        }
    }
}
