using System.IO;

namespace Nicodemus.Controls
{
    public class SelectedFile
    {

        public static readonly SelectedFile Empty = new SelectedFile();

        public string Name { get; set; }

        public long Size { get; set; }

        public FileStream Stream { get; set; }

        public string Extension
        {
            get
            {
                var extension = Path.GetExtension(Name);
                return extension?.Replace(".", "");
            }
        }

        public bool IsValid => !string.IsNullOrWhiteSpace(Name) && Size >= 0 && Stream != null && Stream.Position == 0 && Stream.CanRead;
    }
}
