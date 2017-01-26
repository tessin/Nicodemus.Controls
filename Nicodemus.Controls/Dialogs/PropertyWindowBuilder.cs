using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Nicodemus.Controls.Dialogs
{
    public class PropertyWindowBuilder
    {
        public void Show(object item, string title)
        {
            if (Deployment.Current.Dispatcher.CheckAccess())
                ShowWithoutThreadCheck(item, title);
            else
                Deployment.Current.Dispatcher.BeginInvoke(() => ShowWithoutThreadCheck(item, title));
        }

        private void ShowWithoutThreadCheck(object item, string title)
        {
            new PropertyWindow { DataContext = new PropertyWindowModel(title, GetProperties(item)) }.Show();
        }

        private IEnumerable<PropertyWindowItem> GetProperties(object item)
        {
            return item.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(property => new PropertyWindowItem
                {
                    Title = GetPropertyName(property),
                    Text = GetPropertyValue(property, item)
                });
        }

        private string GetPropertyName(PropertyInfo property)
        {
            return 
                property
                    .GetCustomAttributes(typeof(DisplayAttribute), true).Cast<DisplayAttribute>()
                    .FirstOrDefault()?.Name
                ?? property.Name;
        }

        private string GetPropertyValue(PropertyInfo property, object item)
        {
            var value = property.GetValue(item, null);

            var displayFormatAttribute = property
                   .GetCustomAttributes(typeof(DisplayFormatAttribute), true)
                   .Cast<DisplayFormatAttribute>()
                   .FirstOrDefault();

            return 
                value == null 
                    ? displayFormatAttribute?.NullDisplayText :
                displayFormatAttribute != null && value is IFormattable 
                    ? ((IFormattable)value).ToString(displayFormatAttribute.DataFormatString, CultureInfo.CurrentCulture) :
                value.ToString();
        }
    }
}
