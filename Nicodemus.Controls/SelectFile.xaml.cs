using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
namespace Nicodemus.Controls
{
    public partial class SelectFile : UserControl
    {
        
        public const string DefaultText = "Select a file...";

        private SelectFileWindow SelectFileWindow { get; set; }

        public Stream FileStream { get; private set; }

        public string FileName { get; private set; }

        public long FileSize { get; private set; }

        public event TextChangedEventHandler TextChanged;

        public SelectFile()
        {
            InitializeComponent();
            PathTextBox.TextChanged += PathTextBox_TextChanged;
        }

        public void Clear()
        {
            FileName = null;
            PathTextBox.Text = DefaultText;
            FileStream = null;
            FileSize = 0;
        }

        private SelectFileWindow CreateSelectFileWindow()
        {
            var window = SelectFileWindow.Create("Select a file");
            window.Closed += Window_Closed;
            return window;
        }

        private void PathTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text != DefaultText)
            {
                TextChanged?.Invoke(this, e);
            }
        }

        public void BrowseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Clear();
            SelectFileWindow = CreateSelectFileWindow();
            SelectFileWindow.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var window = sender as SelectFileWindow;
            if (window == null) return;
            if (window.DialogResult == true && (window.DocumentStream != null))
            {
                FileName = window.FileTextBox.Text;
                PathTextBox.Text = FileName;
                FileStream = window.DocumentStream;
                FileSize = window.FileSize;
            }
            else
            {
                Clear();
            }
            window.Closed -= Window_Closed;
        }
    }
}
