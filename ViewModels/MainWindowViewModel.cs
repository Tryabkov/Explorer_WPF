using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        private ObservableCollection<string> _currentPath { get { return Paths.currentPath; } set{ Set(ref Paths.currentPath, value); } }
        private List<string> _upPath { get { return Paths.upPath; } set { Set(ref Paths.upPath, value); } }
        private List<string> _previousPath { get { return Paths.previousPath; } set { Set(ref Paths.previousPath, value); } }
        private List<string> _futurePath { get { return Paths.futurePath; } set { Set(ref Paths.futurePath, value); } }
        private List<string> _folders;
        private List<string> _files;
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
            _currentPath = new ObservableCollection<string>();
            _upPath = new List<string>();
            _previousPath = new List<string>();
            _futurePath = new List<string>();
            #endregion
            Paths.currentPath.CollectionChanged += PathValueChanged;
            Update();
        }

        private void Update()
        {
            FIleOperartor.Update(ListToString(_currentPath));
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

        private string ListToString(IList<string> list)
        {
            string str = string.Empty;
            foreach (var item in list)
            {
                str += item;
            }
            return str;
        }

        private void PathValueChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            UI_CurrentPath = ListToString(Paths.currentPath) + @"\";
            try
            {
                Update();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Access locked", caption: "Warning", button: MessageBoxButton.OK, icon: MessageBoxImage.Warning);
                _currentPath.RemoveAt(_currentPath.Count - 1);
            }
        }

        #region Commads
        public ICommand UpButton_Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {

                }, (obj) => _upPath.Count != 0);
            }
        }

        public ICommand ForwardButton_Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    _currentPath.Add("1");
                }, (obj) => _futurePath.Count != 0);
            }
        }

        public ICommand BackButton_Click
        {
            get
            {
                return new DelegateCommand((obj) =>
                {

                }, (obj) => _previousPath.Count != 0);
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
                        _currentPath.Clear();
                        _currentPath.Add(name);
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
                            if (_currentPath.Count == 1)  _currentPath.Add(((Folder)obj).Name);

                            else _currentPath.Add(@"\" + ((Folder)obj).Name);                           
                        }
                    }
                });
            }
        }

        #endregion
    }
}
