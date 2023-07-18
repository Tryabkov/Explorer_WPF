using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer_WPF.MVVM.Models
{
    internal class Drive
    {
        public string Name { get; set; }
        public DriveInfo Information { get; set; }
        public Drive(string name, DriveInfo information)
        {
            Name = name;
            Information = information;
        }
    }
}
