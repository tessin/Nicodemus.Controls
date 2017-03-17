using System.IO;
using Path = System.IO.Path;

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
}
