using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Explorer_WPF.MVVM.Core;
using Explorer_WPF.MVVM.Models;
using Explorer_WPF.MVVM.Models.AbstractClasses;
using Microsoft.Xaml.Behaviors;

namespace Explorer_WPF.MVVM.ViewModels
{
    internal class MainWindowViewModel : Base.ViewModel
    {
        public TreeView FileStructure { get => _fileStructure; set => _fileStructure = value; }
        public TreeView _fileStructure = new TreeView();

        private bool _isTimerActive = false;
        public MainWindowViewModel()
        {
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

        void SetTreeViewItemTriggers(Control contentControl)
        {
            var b = new Binding("RelativeSource={RelativeSource Self}}");

            var invokeCommandAction = new InvokeCommandAction { CommandParameter = "" };
            var binding = new Binding { Path = new PropertyPath("TreeViewItem_Expanded") };
            BindingOperations.SetBinding(invokeCommandAction, InvokeCommandAction.CommandProperty, binding);

            var eventTrigger = new Microsoft.Xaml.Behaviors.EventTrigger { EventName = "Expanded" };
            eventTrigger.Actions.Add(invokeCommandAction);

            var triggers = Interaction.GetTriggers(contentControl);
            triggers.Add(eventTrigger);


            invokeCommandAction = new InvokeCommandAction { CommandParameter = "BackButton " };
            binding = new Binding { Path = new PropertyPath("TreeViewItem_Collapsed") };
            BindingOperations.SetBinding(invokeCommandAction, InvokeCommandAction.CommandProperty, binding);

            eventTrigger = new Microsoft.Xaml.Behaviors.EventTrigger { EventName = "Collapsed" };
            eventTrigger.Actions.Add(invokeCommandAction);

            triggers = Interaction.GetTriggers(contentControl);
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

        private void AddFoldersToItem(TreeViewStorageItem item)
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

        private void ItemCreated(TreeViewStorageItem item)
        {
            item.OnTreeViewStorageItemCollapced += TreeViewItem_Collapsed;
            item.OnTreeViewStorageItemExpanded += TreeViewItem_Expanded;
        }

        public ICommand TreeView_SelectedItemChanged
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
    
                }/*, (obj) => _upPath.Count != 0*/);
            }
        }

        public void TreeViewItem_Expanded (object sender)
        {
            var ExpandedItem = (TreeViewStorageItem)sender;
            if (ExpandedItem.Items.Count > 0 && ((TreeViewStorageItem)ExpandedItem.Items[0]).Header == "") //It is necessary because method calls multiple times on one click
            {
                ExpandedItem.Items.RemoveAt(0);
                AddFoldersToItem(ExpandedItem);
            }
        }

        public async void TreeViewItem_Collapsed(object sender)
        {
            if (!_isTimerActive)
            {
                var collapsedItem = (TreeViewStorageItem)sender;
                int length = collapsedItem.Items.Count;
                collapsedItem.Items.Clear();
                collapsedItem.Items.Add(new TreeViewFolderItem(""));
                await TimerAsync(1);
            }
        }

        private async Task TimerAsync(int delay)
        {
            _isTimerActive = true;
            await Task.Delay(delay);
            _isTimerActive = false;
        }
    }
}
