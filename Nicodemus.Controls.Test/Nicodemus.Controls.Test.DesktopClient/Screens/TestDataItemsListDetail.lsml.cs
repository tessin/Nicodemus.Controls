using Microsoft.LightSwitch.Presentation.Extensions;
using System.Windows.Controls;

namespace LightSwitchApplication
{
    public partial class TestDataItemsListDetail
    {
        partial void TestDataItemsListDetail_Activated()
        {
            this.FindControl("TextField1").SetBinding(
                TextBox.TextProperty,
                "Value", System.Windows.Data.BindingMode.OneWay);
        }
    }
}