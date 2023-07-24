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
        public static ObservableCollection<Drive> Drives = new ObservableCollection<Drive>();
        public static ObservableCollection<Folder> Folders = new ObservableCollection<Folder>();
        public static ObservableCollection<File> Files = new ObservableCollection<File>();

        public static string[] GetDrives()
        {
            var drives = DriveInfo.GetDrives();
            string[] result= new string[drives.Length];

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

        private static void UpdateFolders(string path)
        {
            Folders.Clear();
            if (!string.IsNullOrEmpty(path))
            {
                foreach (var item in Directory.GetDirectories(path))
                {
                    Folders.Add(new Folder(item));
                }
            }
        }

        private static void UpdateFiles(string path)
        {
            Files.Clear();
            if (!string.IsNullOrEmpty(path))
            {
                foreach (var item in Directory.GetFiles(path))
                {
                    try
                    {
                        Files.Add(new File(item));
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private static void ArrayToList(Array array, IList list)
        {
            list.Clear();
            foreach (var item in array)
            {
                list.Add(item);
            }
        }
    }
}
