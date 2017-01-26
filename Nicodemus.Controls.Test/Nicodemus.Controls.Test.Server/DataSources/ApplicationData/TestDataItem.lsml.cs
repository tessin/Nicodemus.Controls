using Microsoft.LightSwitch;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LightSwitchApplication
{
    public partial class TestDataItem
    {
        partial void ComputedTextField_Compute(ref string result)
        {
            result = $"{TextField} (Computed Property)";
        }
    }
}