using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Nicodemus.Controls.Dialogs
{
    public class PropertyWindowModel
    {
        public string Title { get; set; }
        public ObservableCollection<PropertyWindowItem> Items { get; set; }

        public PropertyWindowModel() { }

        public PropertyWindowModel(string title, IEnumerable<PropertyWindowItem> items)
        {
            Title = title;
            Items = new ObservableCollection<PropertyWindowItem>(items);
        }
    }
}
