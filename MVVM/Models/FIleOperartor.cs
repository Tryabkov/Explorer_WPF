using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Explorer_WPF.MVVM.ViewModels;
using System.Collections.ObjectModel;
using System.Collections;
using System.Windows;

namespace Explorer_WPF.MVVM.Models
{
    internal class FIleOperartor
    {
        public static string[] GetDrives()
        {
            var drives = DriveInfo.GetDrives();
            string[] result = new string[drives.Length];

            for (int i = 0; i < drives.Length; i++)
            {
                result[i] = drives[i].Name;
            }
            return result;
        }

        public static string[] GetFolders(string path)
        {
            string[] Folders;

            try
            {
                Folders = Directory.GetDirectories(path);
            }
            catch (Exception)
            {
                return new string[0];
            }
            return Folders;
        }
        public static string[] GetFiles(string path)
        {
            string[] Folders;
            try
            {
                Folders = Directory.GetFiles(path);
            }
            catch (Exception)
            {
                return new string[0];
            }
            return Folders;
        }
    }
}
