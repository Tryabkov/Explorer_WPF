using System;
using System.Windows;
using System.Windows.Controls;

namespace Explorer_WPF.MVVM.Models
{
    class TreeViewStorageItem : TreeViewItem
    {
        public delegate void AccountHandler(TreeViewStorageItem sender);

        public event AccountHandler? OnTreeViewStorageItemCollapced;
        public event AccountHandler? OnTreeViewStorageItemExpanded;

        public string Path { get; set; }

        private bool _isExpanded;

        public TreeViewStorageItem(string path)
        {
            Path = path;
            this.Collapsed += OnCollapsed;
            this.Expanded += OnExpanded;
        }

        private void OnCollapsed(object sender, RoutedEventArgs e)
        {
            OnTreeViewStorageItemCollapced.Invoke((TreeViewStorageItem)sender);
        }
        private void OnExpanded(object sender, RoutedEventArgs e)
        {
            OnTreeViewStorageItemExpanded.Invoke((TreeViewStorageItem)sender);
        }
    }

    class TreeViewDriveItem : TreeViewStorageItem
    {
        public TreeViewDriveItem(string path) : base(path)
        {
            Header = $"Local Disk ({Path[..^1]})";
        }
    }

    class TreeViewFolderItem : TreeViewStorageItem
    {
        public TreeViewFolderItem(string path) : base(path)
        {
            Header = path[(path.LastIndexOf('\\') + 1)..];
        }
    }
}
