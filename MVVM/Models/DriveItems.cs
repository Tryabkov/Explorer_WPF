using System;
using System.Windows;
using System.IO;

namespace Explorer_WPF.MVVM.Models
{
    abstract class BaseItem
    {
        public string Name { get; protected set; }
        public string Path { get; protected set; }
        public string Type { get; protected set; }
        public bool IsFile { get; protected set; }
        public DateTime? Created { get; protected set; }
        public DateTime? Modified { get; protected set; }
        public DateTime? Accessed { get; protected set; }
        public string ImageSourse { get; protected set; }

        public BaseItem(string path)
        {
            Path = path;
            Created = Directory.GetCreationTime(path);
            Modified = Directory.GetLastAccessTime(path);
            Accessed = Directory.GetLastWriteTime(path);
        }
    }  

    internal class File : BaseItem
    {
        public File(string path) : base(path)
        {
            int slashIndex = path.LastIndexOf('\\');
            int dotIndex = path.LastIndexOf('.');
            Name = path.Substring(slashIndex + 1);
            Type = path[dotIndex..].ToUpper() + " File";
            IsFile = true;
            ImageSourse = "/resources/File_IMG.png";
        }

        public void Open()
        {
            try
            {
                var process = new System.Diagnostics.Process();
                process.StartInfo.FileName = Path;
                process.StartInfo.UseShellExecute = true;
                process.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("File can`t be open", "Warning", button: MessageBoxButton.OK, icon: MessageBoxImage.Warning);
            }

        }
    }

    internal class Folder : BaseItem
    {
        public Folder(string path) : base(path)
        {
            Name = path[(path.LastIndexOf('\\') + 1)..];
            Type = "Folder";
            IsFile = false;
            ImageSourse = "/resources/Folder_IMG.png";
        }
    }
}
