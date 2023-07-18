using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer_WPF.MVVM.Models.AbstractClasses;

namespace Explorer_WPF.MVVM.Models
{
    internal class Folder : FolderItem
    {
        public Folder(string path)
        {
            IsFile = false;
            Path = path;
            Name = GetName(path);
            CreationTime = Directory.GetCreationTime(path);
            LastAccessTime = Directory.GetLastAccessTime(path);
            LastWriteTime = Directory.GetLastWriteTime(path);
        }

        private string GetName(string path)
        {
            return path.Substring(path.LastIndexOf(@"\") + 1);
        }
    }
}
