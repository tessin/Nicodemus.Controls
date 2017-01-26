using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.LightSwitch.Presentation.Framework;
using Microsoft.LightSwitch.Presentation.Implementation.Controls;
using Nicodemus.Controls.Helpers;

namespace Nicodemus.Controls
{
    public class LinkButton : HyperlinkButton
    {
        public LinkButton()
        {
            Loaded += (s, e) =>
            {
                SetBinding(ContentProperty, new Binding("Value") { Mode = BindingMode.OneWay });
                LightswitchEnable();
            };
        }

        // If the underlying property is read-only, Lightswitch will set IsEnabled = false for the container controls
        // which will render this control unusable.
        // This method fixes the problem by setting IsEnabled on the container controls to true.
        private void LightswitchEnable()
        {
            var contentControl = VisualTreeHelperExt.FindParent<ValueCustomContentControl>(this);
            var presenter = VisualTreeHelperExt.FindParent<ContentItemPresenter>(this);

            if (contentControl != null && presenter != null)
                contentControl.IsEnabled = presenter.IsEnabled = true;
        }
    }
}
