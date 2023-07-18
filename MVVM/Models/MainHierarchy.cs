using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer_WPF.MVVM.Models
{

    internal class MainHierarchy
    {
        public class Node
        {
            public string Name { get; set; }
            public ObservableCollection<Node> Nodes { get; set; }
        }


        public ObservableCollection<Node> nodes = new ObservableCollection<Node>
        {
            new Node
            {
                Name="1",
                Nodes = new ObservableCollection<Node>
                {
                    new Node
                    {
                        Name="11",
                        Nodes= new ObservableCollection<Node>
                        {
                            new Node{Name="111" },
                            new Node{Name="112" },
                            new Node{Name="113" },
                            new Node{Name="114" }
                        }
                    },
                    new Node {Name="12" },
                    new Node {Name="13" },
                    new Node {Name="14" },
                }
            },
            new Node{
                Name="2",
                Nodes = new ObservableCollection<Node>
                {
                    new Node {Name="21" },
                    new Node {Name="22" },
                    new Node {Name="23" },
                    new Node {Name="24" },
                }
            },
            new Node
            {
                Name="3",
                Nodes = new ObservableCollection<Node>
                {
                    new Node {Name="31" },
                    new Node {Name="32" },
                    new Node {Name="33" },
                    new Node {Name="34" },
                }
            },
            new Node{
                Name="4",
                Nodes = new ObservableCollection<Node>
                {
                    new Node {Name="41" },
                    new Node {Name="42" },
                    new Node {Name="43" },
                    new Node {Name="44" },
                }
            },
        };

    }
}
