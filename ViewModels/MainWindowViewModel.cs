using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Input;
using Explorer_WPF.Models;
using Explorer_WPF.Models.AbstractClasses;

namespace Explorer_WPF.ViewModels
{
    internal class MainWindowViewModel : Base.ViewModel
    {
        #region New instanses
        Paths Paths = new Paths();
        #endregion

        #region Program fields

        #endregion

        #region UI fields

        private string _UI_CurrentPath;
        public string UI_CurrentPath { get { return _UI_CurrentPath; } set { Set(ref _UI_CurrentPath, value); OnPropertyChanged(); } }

        private string _searchTextBox;
        public string SearchTextBox { get { return _searchTextBox; } set { Set(ref _searchTextBox, value); OnPropertyChanged(); } }

        private ObservableCollection<Drive> _drives = new ObservableCollection<Drive>();
        public ObservableCollection<Drive> Drives { get { return _drives; } private set { Set(ref _drives, value); OnPropertyChanged(); } }

        private ObservableCollection<FolderItem> _foldersAndFiles = new ObservableCollection<FolderItem>();
        public ObservableCollection<FolderItem> FoledrsAndFiles { get { return _foldersAndFiles; } set { Set(ref _foldersAndFiles, value); OnPropertyChanged(); } }

        #endregion
        public MainWindowViewModel()
        {
            #region Setting values
            _UI_CurrentPath = string.Empty;
            _searchTextBox = string.Empty;
            #endregion
            //Paths.currentPath.CollectionChanged += PathValueChanged;
            Update();
        }

        private void Update()
        {
            FIleOperartor.Update(ListToString(Paths.currentPath));
            Drives = FIleOperartor.Drives;

            var foledrsAndFiles = new ObservableCollection<FolderItem>();

            foreach (var item in FIleOperartor.Folders)
            {
                foledrsAndFiles.Add(item);
            }
            foreach (var item in FIleOperartor.Files)
            {
                foledrsAndFiles.Add(item);
            }
            FoledrsAndFiles = foledrsAndFiles;
        }

        private void PathValueChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            UI_CurrentPath = ListToString(Paths.currentPath);
            try
            {
                Update();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Access locked", caption: "Warning", button: MessageBoxButton.OK, icon: MessageBoxImage.Warning);
                Paths.currentPath.RemoveAt(Paths.currentPath.Count - 1);
                //TODO: also remove in pathHisory ande index
            }
        }

        private string ListToString(IList<string> list)
        {
            string str = string.Empty;
            foreach (var item in list)
            {
                str += item;
            }
            return str;
        }

        private void CurrentPathChanged()
        {
            Paths.CurrentPathChanged();
            Update();
        }

        #region Commads
        public ICommand UpButton_Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Paths.currentPath.Clear();
                    for (int i = 0; i < Paths.upPath.Count; i++)
                    {
                        Paths.currentPath.Add(Paths.upPath[i]);
                    }
                    CurrentPathChanged();
                    //var lastCurrentPathItem = Paths.upPath[Paths.upPath.Count - 1];
                    /*Paths.currentPath.Add(lastCurrentPathItem); *///this crutch is needed because observable collection doesn`t see changes 

                }, (obj) => Paths.upPath.Count != 0);
            }
        }

        public ICommand ForwardButton_Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {

                }, (obj) => Paths.futurePath.Count != 0);
            }
        }

        public ICommand BackButton_Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {

                }, (obj) => Paths.previousPath.Count != 0);
            }
        }

        public ICommand MainTreeView_SelectedItemChanged
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    var name = ((Drive)obj)?.Information.Name;
                    if (name != null)
                    {
                        Paths.currentPath.Clear();
                        Paths.currentPath.Add(name);
                        CurrentPathChanged();
                    }
                });
            }
        }

        public ICommand DataGrid_DoubleClick
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (obj != null)
                    {
                        if (((FolderItem)obj).IsFile) ((File)obj).Open();

                        else
                        {
                            if (Paths.currentPath.Count == 1) Paths.currentPath.Add(((Folder)obj).Name + @"\");

                            else Paths.currentPath.Add(((Folder)obj).Name + @"\");

                            CurrentPathChanged();
                        }
                    }
                });
            }
        }
        #endregion
    }
}
