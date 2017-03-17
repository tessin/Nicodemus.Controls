using System.Windows;
using System.Windows.Controls;
namespace Nicodemus.Controls
{
    public partial class SelectFile : UserControl
    {
        
        public const string DefaultText = "Select a file...";

        //private SelectFileWindow SelectFileWindow { get; set; }

        private FileSelector Selector { get; set; }

        public SelectedFile File { get; set; }

        public event TextChangedEventHandler TextChanged;

        public static string DefaultFilter { get; set; }

        public string Filter { get; set; }

        static SelectFile()
        {
            DefaultFilter = "Any file (*.*)|*.*";
        }

        public SelectFile()
        {
            Selector = new FileSelector(DefaultFilter);
            File = new SelectedFile();
            InitializeComponent();
            PathTextBox.TextChanged += PathTextBox_TextChanged;
        }

        public void Clear()
        {
            PathTextBox.Text = DefaultText;
            File = SelectedFile.Empty;
            //File.Name = null;
            //File.Stream = null;
            //File.Size = 0;
        }

        //private SelectFileWindow CreateSelectFileWindow()
        //{
        //    var window = SelectFileWindow.Create("Select a file");
        //    window.Closed += Window_Closed;
        //    return window;
        //}

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
            Selector.Filter = Filter ?? DefaultFilter;
            File = Selector.Select();
            
            if (File.IsValid)
            {
                PathTextBox.Text = File.Name;
            }
            else
            {
                Clear();
            }

            //SelectFileWindow = CreateSelectFileWindow();
            //SelectFileWindow.Show();
        }

        //private void Window_Closed(object sender, EventArgs e)
        //{
        //    var window = sender as SelectFileWindow;
        //    if (window == null) return;
        //    if (window.DialogResult == true && (window.File.Stream != null))
        //    {
        //        File.Name = window.FileTextBox.Text;
        //        PathTextBox.Text = File.Name;
        //        File.Stream = window.File.Stream;
        //        File.Size = window.File.Size;
        //    }
        //    else
        //    {
        //        Clear();
        //    }
        //    window.Closed -= Window_Closed;
        //}
    }
}
