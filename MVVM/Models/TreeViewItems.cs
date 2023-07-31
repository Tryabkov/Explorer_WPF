using System;
using System.Windows;
using System.Windows.Controls;

namespace Explorer_WPF.MVVM.Models
{
    class TreeViewBaseItem : TreeViewItem
    {
        public delegate void AccountHandler(TreeViewBaseItem sender);

        public event AccountHandler? OnTreeViewStorageItemCollapced;
        public event AccountHandler? OnTreeViewStorageItemExpanded;

        public string Path { get; set; }

        public TreeViewBaseItem(string path)
        {
            Path = path;
        }

        private void OnCollapsed(object sender, RoutedEventArgs e)
        {
            OnTreeViewStorageItemCollapced.Invoke((TreeViewBaseItem)sender);
        }
        private void OnExpanded(object sender, RoutedEventArgs e)
        {
            OnTreeViewStorageItemExpanded.Invoke((TreeViewBaseItem)sender);
        }
    }

    class TreeViewDriveItem : TreeViewBaseItem
    {
        public TreeViewDriveItem(string path) : base(path)
        {
            Header = $"Local Disk ({Path[..^1]})";
        }
    }

    class TreeViewFolderItem : TreeViewBaseItem
    {
        public TreeViewFolderItem(string path) : base(path)
        {
            Header = path[(path.LastIndexOf('\\') + 1)..];
        }
    }
}
