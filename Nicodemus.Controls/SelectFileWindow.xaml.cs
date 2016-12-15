using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Nicodemus.Controls
{


    public delegate void FileAvailableDelegate(object source, FileAvailableEventArgs args);

    public class FileAvailableEventArgs
    {

        public FileStream FileStream { get; set; }

        public string FileName { get; set; }

        public string Extension
        {
            get
            {
                var extension = Path.GetExtension(FileName);
                return extension?.Replace(".", "");
            }
        }

        public FileAvailableEventArgs(FileStream fileStream, string fileName)
        {
            FileStream = fileStream;
            FileName = fileName;
        }

    }

    public partial class SelectFileWindow : ChildWindow
    {
        public SelectFileWindow()
        {
            Filter = "pdf files (*.pdf)|*.pdf";
            InitializeComponent();
        }
        
        public string FileName { get; private set; }

        public string Extension
        {
            get
            {
                var extension = Path.GetExtension(FileName);
                return extension?.Replace(".", "");
            }
        }

        public long FileSize { get; private set; }

        public FileStream DocumentStream { get; private set; }

        public string Filter { get; set; }

        public event FileAvailableDelegate FileAvailable;

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
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = Filter;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() != true) return;
            FileTextBox.Text = openFileDialog.File.Name;
            FileName = FileTextBox.Text;
            FileSize = openFileDialog.File.Length;
            FileTextBox.IsReadOnly = true;
            var myStream = openFileDialog.File.OpenRead();
            DocumentStream = myStream;
            OnFileAvailable();
        }

        public static SelectFileWindow Create(string title, string filter = null)
        {
            var window = new SelectFileWindow();
            if (filter != null) window.Filter = filter;
            window.Title = title;
            return window;
        }

        private void OnFileAvailable()
        {
            FileAvailable?.Invoke(this, new FileAvailableEventArgs(DocumentStream, FileName));
        }

    }
}



