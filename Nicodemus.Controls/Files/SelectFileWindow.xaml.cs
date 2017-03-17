using System.Windows;
using System.Windows.Controls;

namespace Nicodemus.Controls
{
    
    public partial class SelectFileWindow : ChildWindow
    {

        private FileSelector FileSelector { get; }

        public SelectedFile File { get; private set; }

        public event FileAvailableDelegate FileAvailable;

        public SelectFileWindow(string filter)
        {
            File = SelectedFile.Empty;
            if (filter == null) filter = "Any file (*.*)|*.*";
            InitializeComponent();
            FileSelector = new FileSelector(filter);

        }

        public SelectFileWindow(): this(null)
        {
        }
        
        /// <summary>
        /// OK Button
        /// </summary>
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        
        /// <summary>
        /// Cancel button
        /// </summary>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// Browse button
        /// </summary>
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            File = FileSelector.Select();
            FileTextBox.Text = File.Name ?? "";
            if (File.IsValid) OKButton.IsEnabled = true;
            OnFileAvailable();
        }

        public static SelectFileWindow Create(string title, string filter = null)
        {
            var window = new SelectFileWindow(filter);
            window.Title = title;
            return window;
        }

        private void OnFileAvailable()
        {
            FileAvailable?.Invoke(this, new FileAvailableEventArgs(File.Stream, File.Name));
        }

    }
}



