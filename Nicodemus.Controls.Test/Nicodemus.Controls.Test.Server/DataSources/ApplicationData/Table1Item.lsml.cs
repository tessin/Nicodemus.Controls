using Microsoft.LightSwitch;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LightSwitchApplication
{
    public partial class Table1Item
    {
        partial void Property1_Compute(ref string result)
        {
            result = "value is computed thus IsEnabled is false";
        }
    }
}