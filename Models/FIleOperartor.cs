using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Explorer_WPF.ViewModels;
using System.Collections.ObjectModel;

namespace Explorer_WPF.Models
{
    internal class FIleOperartor
    {
        //private delegate EventHandler(List<string> previousPath, List<string> currentPath, List<string> futurePath, List<string> upPath, List<List<string>> pathHistory, ref int pathHistoryIndex);
        //public static event EventHandler PathChanged;

        public static ObservableCollection<Drive> GetDrives()
        {
            DriveInfo[] drivesInfo = DriveInfo.GetDrives();
            ObservableCollection<Drive> drives = new ObservableCollection<Drive>();
            for (int i = 0; i < drivesInfo.Length; i++)
            {
                drives.Add(new Drive(drivesInfo[i].Name.Substring(0, drivesInfo[i].Name.Length - 1), drivesInfo[i]));
            }
            return drives;
        }

        public static string[] GetDirectory(string path)
        {
            return Directory.GetFiles(path);
        }

        public static string[] GetFiles(string path)
        {
            return Directory.GetDirectories(path);
        }

        private  List<string> ArrayToList(DriveInfo[] array)
        {   
            List<string> list = new List<string>();
            foreach (var item in array)
            {
                
            }
            return list;
        }
    }
}
