using System.Windows;
using System.Windows.Controls;
using Microsoft.LightSwitch.Threading;

namespace Nicodemus.Controls.Busy
{
    public partial class WaitWindow : ChildWindow
    {

        private static WaitWindow _instance;

        public static void Show(string title, string message)
        {
            Dispatchers.Main.BeginInvoke(() =>
            {
                _instance = new WaitWindow();
                _instance.Title = title;
                //_instance.MessageBox.Text = message;
                ((ChildWindow)_instance).Show();
            });
        }

        public new static void Show()
        {
            Dispatchers.Main.BeginInvoke(() =>
            {
                _instance = new WaitWindow();
                ((ChildWindow) _instance).Show();
            });
        }

        public new static void Close()
        {
            Dispatchers.Main.BeginInvoke(() =>
            {
                (_instance as ChildWindow)?.Close();
            });
        }

        public WaitWindow()
        {
            InitializeComponent();
        }

    }
}

