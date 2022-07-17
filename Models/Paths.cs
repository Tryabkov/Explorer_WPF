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
        private int _pathHistoryIndex = -1;
        #endregion

        public Paths()
        {
            //currentPath.CollectionChanged += CurrentPathChanged;
        }

        public void CurrentPathChanged()
        {
            if (currentPath.Count > 0)
            {
                var historyItem = new Collection<string>();
                foreach (var item in currentPath)
                {
                    historyItem.Add(item);
                }
                _pathHistory.Add(historyItem);
                _pathHistoryIndex++;
            }
            previousPath.Clear();
            if (_pathHistory?.Count >= 2 && _pathHistory.Count > _pathHistoryIndex)
            {
                previousPath = new(_pathHistory[_pathHistoryIndex - 1]);
            }
            futurePath.Clear();
            if (_pathHistory?.Count >= 2 && _pathHistory.Count - 1 > _pathHistoryIndex)
            {
                futurePath = new(_pathHistory[_pathHistoryIndex + 1]);
            }
            upPath.Clear();
            if (currentPath.Count >= 2)
            {
                for (int i = 0; i < currentPath.Count - 1; i++)
                {
                    upPath?.Add(currentPath[i]);
                }
            }
        }
    }
}
