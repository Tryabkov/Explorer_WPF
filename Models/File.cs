using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Explorer_WPF.Models.AbstractClasses;

namespace Explorer_WPF.Models
{
    internal class File : FolderItem
    {
        public File(string path)
        {
            IsFile = true;
            Path = path;
            Name = GetName(path);
            Extention = GetExtension(path);
        }

        private string GetName(string path)
        {
            return path.Substring(path.LastIndexOf(@"\") + 1, path.LastIndexOf('.') - (path.LastIndexOf(@"\") + 1));            
        }

        private string GetExtension(string path)
        {
            return path.Substring(path.LastIndexOf('.'));
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
                MessageBox.Show("File can`t be open", "Warning", button:MessageBoxButton.OK, icon:MessageBoxImage.Warning);
            }
            
        }
    }
}
