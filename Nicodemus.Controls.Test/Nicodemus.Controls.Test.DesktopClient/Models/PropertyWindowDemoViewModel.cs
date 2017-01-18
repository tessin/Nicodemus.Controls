using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.LightSwitch.Model.Storage;

namespace LightSwitchApplication.Models
{

    public class PropertyWindowDemoViewModel
    {

        [Display(Name = "Title")]
        public string TitleString { get; set; } = "Tessin";

        public string Foo { get; set; } = "Hello"; //Display.Name is Foo

        public double Double { get; set; } = 33.33;

        public int Int { get; set; } = 1337;

        public bool Bool { get; set; } = true;

        [DisplayFormat(DataFormatString = "C0")]
        public long Long { get; set; } = long.MaxValue;

        [DisplayFormat(DataFormatString = "yyy-MM-dd")]
        public DateTime DateTime { get; set; } = DateTime.UtcNow;

        [DisplayFormat(DataFormatString = "yyy-MM-dd", NullDisplayText = "?")]
        public DateTime? DateTimeNullable { get; set; } = null;

    }
}
