using Microsoft.LightSwitch.Presentation.Implementation;
using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace Nicodemus.Controls.Common
{
    // note that when you base you control on the UserContentItemControl
    // the data context is a LightSwitch ContentItem, if you want to bind
    // to properties that you introduce on your control, you need to use
    // the following data binding syntax
    // {Binding Path=<dependency-property-name>, RelativeSource={RelativeSource AncestorType=UserControl}}
    // the RelativeSource={RelativeSource AncestorType=UserControl} part is what tells Silverlight to bind
    // with the UserControl as context and not the DataContext itself

    public abstract class UserContentItemControl : UserControl
    {
        private readonly PropertyChangedEventHandler _contentItemPropertyChanged;

        public ContentItem ContentItem { get; private set; }

        public UserContentItemControl()
        {
            Loaded += (sender, e) =>
            {
                OnLoaded();
            };

            DataContextChanged += (sender, e) =>
            {
                for (var oldContentItem = e.OldValue as ContentItem; oldContentItem != null;)
                {
                    if (!(ContentItem == oldContentItem))
                    {
                        throw new InvalidOperationException();
                    }
                    oldContentItem.PropertyChanged -= _contentItemPropertyChanged;
                    OnContentItemUnsubscribed(oldContentItem);
                    ContentItem = null;
                    break;
                }
                for (var newContentItem = e.NewValue as ContentItem; newContentItem != null;)
                {
                    if (!(ContentItem == null))
                    {
                        throw new InvalidOperationException();
                    }
                    newContentItem.PropertyChanged += _contentItemPropertyChanged;
                    ContentItem = newContentItem;
                    OnContentItemSubscribed(newContentItem);
                    break;
                }
            };

            _contentItemPropertyChanged = new PropertyChangedEventHandler((sender, e) =>
            {
                switch (e.PropertyName)
                {
                    case "Value":
                        var contentItem = (ContentItem)sender;
                        OnContentItemValueChanged(contentItem, contentItem.Value);
                        break;
                }
            });

            Unloaded += (sender, e) =>
            {
                var contentItem = ContentItem;
                if (contentItem != null)
                {
                    contentItem.PropertyChanged -= _contentItemPropertyChanged;
                }
                OnUnloaded();
            };
        }

        protected abstract void OnLoaded();

        protected abstract void OnContentItemSubscribed(ContentItem contentItem);

        protected abstract void OnContentItemValueChanged(ContentItem contentItem, object value);

        protected abstract void OnContentItemUnsubscribed(ContentItem contentItem);

        protected abstract void OnUnloaded();
    }
}
