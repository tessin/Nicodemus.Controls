using System.Windows;
using System.Windows.Media;

namespace Nicodemus.Controls.Common
{
    public static class VisualTreeHelperExt
    {
        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            var curItem = child;
            while (curItem != null)
            {
                curItem = VisualTreeHelper.GetParent(curItem);
                if (curItem is T) return (T)curItem;
            }
            return null;
        }
    }
}
