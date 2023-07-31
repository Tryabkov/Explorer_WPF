using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Explorer_WPF.MVVM.Core;
using Explorer_WPF.MVVM.Models;
using Microsoft.Xaml.Behaviors;

namespace Explorer_WPF.MVVM.ViewModels
{
    internal class MainWindowViewModel : Base.ViewModel
    {
        public TreeView FileStructure { get => _fileStructure; set => _fileStructure = value; }
        private TreeView _fileStructure = new TreeView();

        public ObservableCollection<BaseItem> FolderContent { get => _folderContent; set { _folderContent = value; OnPropertyChanged(); } }
        private ObservableCollection<BaseItem> _folderContent = new ObservableCollection<BaseItem>();

        public List<string> CurrentPath { get; set; }
        public ObservableCollection<ObservableCollection<string>> PathHistory { get; set; }
        private int PathPlace { get; set; } = 0;

        public MainWindowViewModel()
        {
            CurrentPath = new List<string>();
            PathHistory = new ObservableCollection<ObservableCollection<string>>();
            _fileStructure.Name = "MainTreeView";
            SetTreeViewTriggers(_fileStructure);
            AddDrivesToFileStructure();
            
        }

        void SetTreeViewTriggers(Control contentControl)
        {
            // create the command action and bind the command to it
            var invokeCommandAction = new InvokeCommandAction { CommandParameter = "" };
            var binding = new Binding { Path = new PropertyPath("TreeView_SelectedItemChanged") };
            BindingOperations.SetBinding(invokeCommandAction, InvokeCommandAction.CommandProperty, binding);

            // create the event trigger and add the command action to it
            var eventTrigger = new Microsoft.Xaml.Behaviors.EventTrigger { EventName = "SelectedItemChanged" };
            eventTrigger.Actions.Add(invokeCommandAction);

            // attach the trigger to the control
            var triggers = Interaction.GetTriggers(contentControl);
            triggers.Add(eventTrigger);
        }

        private void AddDrivesToFileStructure()
        {
            string[] drives = FIleOperartor.GetDrives();
            foreach (var drive in drives)
            {
                var item = new TreeViewDriveItem(drive);
                item.Items.Add(new TreeViewFolderItem("")); //For small arrow to the left of header
                FileStructure.Items.Add(item);
                
                ItemCreated(item);
            }
        }

        private void AddFoldersToTreeVeiwItem(TreeViewBaseItem item)
        {
            string[] Folders = FIleOperartor.GetFolders(item.Path);
            foreach (var folder in Folders)
            {
                var newItem = new TreeViewFolderItem(folder);
                if (FIleOperartor.GetFolders(folder).Length > 0)
                {
                    newItem.Items.Add(new TreeViewFolderItem("")); //For small arrow to the left of header
                }
                item.Items.Add(newItem);

                ItemCreated(newItem);
            }
        }
        private void AddContentToFolderContent(string path)
        {
            var folders = FIleOperartor.GetFolders(path);
            var files = FIleOperartor.GetFiles(path);
            FolderContent.Clear();
            foreach (var item in folders) FolderContent.Add(new Folder(item));
            foreach (var item in files) FolderContent.Add(new File(item));
        }

        private string ListToString(List<string> list) => string.Join(null, list);

        private void ItemCreated(TreeViewBaseItem item)
        {
            item.Collapsed += TreeViewItem_Collapsed;
            item.Expanded += TreeViewItem_Expanded;
            item.Selected += TreeViewItem_Selected;
        }

        public void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            var path = ((TreeViewBaseItem)sender).Path;
            if (path == ((TreeViewBaseItem)e.Source).Path)
            {
                AddContentToFolderContent(path);
                CurrentPath.Clear();
                string[] itm = path.Split('\\');
                    
                foreach (var pathItem in itm)
                {
                    CurrentPath.Add(pathItem + '\\');
                }
            }
        }

        public void TreeViewItem_Expanded (object sender, RoutedEventArgs e)
        {
            var ExpandedItem = (TreeViewBaseItem)sender;
            if (ExpandedItem.Items.Count > 0 && ((TreeViewBaseItem)ExpandedItem.Items[0]).Header == "") //It is necessary because method calls multiple times on one click
            {
                ExpandedItem.Items.RemoveAt(0);
                AddFoldersToTreeVeiwItem(ExpandedItem);
            }
        }

        public void TreeViewItem_Collapsed(object sender, RoutedEventArgs e)
        {
            if (((TreeViewBaseItem)sender).Path == ((TreeViewBaseItem)e.Source).Path)
            {
                var collapsedItem = (TreeViewBaseItem)sender;
                collapsedItem.Items.Clear();
                collapsedItem.Items.Add(new TreeViewFolderItem(""));
            }
        }


        public ICommand DataGrid_DoubleClick
        {
            get
            {
                return new DelegateCommand((obj)  =>
                {
                    var selectedItem = (BaseItem)obj;
                   if (!selectedItem.IsFile)
                   {
                        AddContentToFolderContent(selectedItem.Path);
                        CurrentPath.Add(selectedItem.Path.Substring(selectedItem.Path.LastIndexOf('\\')));
                   }
                   else Console.WriteLine();
                });
            }
        }

        public ICommand UpButton_Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    CurrentPath.RemoveAt(CurrentPath.Count - 1);
                    AddContentToFolderContent(ListToString(CurrentPath));
                }, obj => CurrentPath.Count > 1);
            }
        }

        public ICommand BackButton_Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {

                }, obj => true);
            }
        }

        public ICommand ForwardButton_Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {

                }, obj => PathHistory.Count < PathPlace);
            }
        }
    }
}
