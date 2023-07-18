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
        //private delegate EventHandler(List<string> previousPath, List<string> currentPath, List<string> futurePath, List<string> upPath, List<List<string>> pathHistory, ref int pathHistoryIndex);
        //public static event EventHandler PathChanged;
        public static ObservableCollection<Drive> Drives = new ObservableCollection<Drive>();
        public static ObservableCollection<Folder> Folders = new ObservableCollection<Folder>();
        public static ObservableCollection<File> Files = new ObservableCollection<File>();

        public static void Update(string path)
        {
            UpdateDrives();
            UpdateFolders(path);
            UpdateFiles(path);
        }

        private static void UpdateDrives()
        {
            Drives.Clear();

            DriveInfo[] drivesInfo = DriveInfo.GetDrives();
            for (int i = 0; i < drivesInfo.Length; i++)
            {
                Drives.Add(new Drive(drivesInfo[i].Name.Substring(0, drivesInfo[i].Name.Length - 1), drivesInfo[i]));
            }
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
