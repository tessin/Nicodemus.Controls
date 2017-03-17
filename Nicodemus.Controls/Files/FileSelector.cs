using System.Windows.Controls;

namespace Nicodemus.Controls
{
    public class FileSelector
    {
        
        public string Filter { get; set; }
        
        public FileSelector(string filter)
        {
            Filter = filter;
        }

        public SelectedFile Select()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = Filter,
                FilterIndex = 1
            };
            if (openFileDialog.ShowDialog() != true) return SelectedFile.Empty;
            var selected = new SelectedFile
            {
                Name = openFileDialog.File.Name,
                Size = openFileDialog.File.Length,
                Stream = openFileDialog.File.OpenRead()
            };
            return selected;
        }

    }
}
