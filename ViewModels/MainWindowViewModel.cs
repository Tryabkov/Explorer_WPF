using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Explorer_WPF.Models;
using static Explorer_WPF.Models.MainHierarchy;

namespace Explorer_WPF.ViewModels
{
    internal class MainWindowViewModel : Base.ViewModel
    {
        #region New instanses
        Paths Paths = new Paths();
        #endregion

        #region Program fields
        private ObservableCollection<string> _currentPath { get { return Paths.currentPath; } set{ Paths.currentPath = value; } }
        private List<string> _upPath { get { return Paths.upPath; } set { Paths.upPath = value; } }
        private List<string> _previousPath { get { return Paths.previousPath; } set { Paths.previousPath = value; } }
        private List<string> _futurePath { get { return Paths.futurePath; } set { Paths.futurePath = value; } }
        #endregion

        #region UI fields

        private string _UI_CurrentPath;
        public string UI_CurrentPath { get { return _UI_CurrentPath; } set { Set(ref _UI_CurrentPath, value); OnPropertyChanged(); } }

        private string _searchTextBox;
        public string SearchTextBox { get { return _searchTextBox; } set { Set(ref _searchTextBox, value); OnPropertyChanged(); } }

        private ObservableCollection<Drive> _mainTreeView = new ObservableCollection<Drive>();
        public ObservableCollection<Drive> MainTreeView { get { return _mainTreeView; } private set { Set(ref _mainTreeView, value); OnPropertyChanged(); } }
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
            MainTreeView = FIleOperartor.GetDrives();
        }

        private string ListToString(ICollection<string> list)
        {
            string str = string.Empty;
            foreach (var item in list)
            {
                ObservableCollection<string> cp = new ObservableCollection<string>();
                str += item;
            }
            return str;
        }

        private void PathValueChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            UI_CurrentPath = ListToString(Paths.currentPath);
        }

        #region Button`s commads
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

        public ICommand MouseDoubleClick
        {
            get
            {
                return new DelegateCommand((obj) =>
                {

                });
            }
        }


        #endregion
    }
}
