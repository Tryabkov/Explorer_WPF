using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Explorer_WPF.MVVM;
using System.Windows;
using System.Windows.Controls;
using System.Collections;
using System.Windows.Media;

namespace Explorer_WPF.MVVM.Models
{
    internal class CustomButton : Button
    {
        public readonly string Path;
        public CustomButton(string path)
        {
            Path = path;
            Content = path[(path.LastIndexOf('\\')+1)..];
            Background = Brushes.White;
            BorderBrush = Brushes.White;
            FontSize = 18;
            Margin = new Thickness(0, 0, 5, -5);
            Padding = new Thickness(0,0,0,5);
        }
    }
}
