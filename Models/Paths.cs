using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer_WPF.Models
{
    internal struct Paths
    {
        #region Filds
        public ObservableCollection<string> currentPath = new ObservableCollection<string>();
        public List<string> previousPath = new List<string>();
        public List<string> upPath = new List<string>();
        public List<string> futurePath = new List<string>();
        private List<Collection<string>> _pathHistory = new List<Collection<string>>();
        private int _pathHistoryIndex = 0;
        #endregion

        public Paths()
        {
            currentPath.CollectionChanged += CurrentPathChanged;
        }

        private void CurrentPathChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            _pathHistory.Add(currentPath);
            _pathHistoryIndex++;

            previousPath.Clear();
            if (_pathHistory?.Count >= 2 && _pathHistory.Count > _pathHistoryIndex)
            {
                previousPath = new(_pathHistory[_pathHistoryIndex - 1]);
            }
            futurePath.Clear();
            if (_pathHistory?.Count >= 2 && _pathHistory.Count > _pathHistoryIndex)
            {
                futurePath = new(_pathHistory[_pathHistoryIndex + 1]);
            }

            upPath.Clear();
            if (currentPath.Count >= 2)
            {
                for (int i = 0; i < currentPath.Count - 2; i++)
                {
                    upPath?.Add(currentPath[i]);
                }
            }
        }
    }
}
