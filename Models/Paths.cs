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
        private List<List<string>> _pathHistory = new List<List<string>>();
        private int _pathHistoryIndex = 0;
        #endregion

        public Paths()
        {
            currentPath.CollectionChanged += UpdatePaths;
        }

        private void UpdatePaths(object? sender, NotifyCollectionChangedEventArgs e)
        {
            previousPath.Clear();
            if (_pathHistory?.Count >= 2 && _pathHistory.Count > _pathHistoryIndex)
            {
                previousPath = _pathHistory[_pathHistoryIndex --];
            }
            futurePath.Clear();
            if (_pathHistory?.Count >= 2 && _pathHistory.Count > _pathHistoryIndex)
            {
                futurePath = _pathHistory[_pathHistoryIndex ++];
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
