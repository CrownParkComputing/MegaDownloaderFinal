using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;
using System.Linq;
using CG.Web.MegaApiClient;

namespace MegaDownloaderFinal.ViewModels
{
    public class NodeListingViewModel : ViewModelBase
    {
        
        private readonly MegaApiClient client = new();

        public ICollectionView NodesCollectionView { get; }

        private string _nodesFilter = string.Empty;
        
        NodeViewModel _nodeViewModel;

        ObservableCollection<NodeViewModel> _nodesCollection = new();
        public string NodesFilter
        {
            get
            {
                return _nodesFilter;
            }
            set
            {
                _nodesFilter = value;
                OnPropertyChanged(nameof(NodesFilter));
                NodesCollectionView.Refresh();
            }
        }
        

        
        public NodeListingViewModel()
        {
            {

                    
            Uri folderLink = new Uri("https://mega.nz/folder/gdozjZxL#uI5SheetsAd-NYKMeRjf2A");
            client.LoginAnonymous();

            IEnumerable<INode> nodes = client.GetNodesFromLink(folderLink);
            INode parent = nodes.Single(n => n.Type == NodeType.Root);
            _nodeViewModel = new NodeViewModel(parent.Id, parent.Name, false);
            GetNodesRecursive(_nodeViewModel, nodes, parent);

            _nodesCollection.Add(_nodeViewModel);
            client.Logout(); 


            NodesCollectionView = CollectionViewSource.GetDefaultView(_nodesCollection);

            NodesCollectionView.Filter = FilterNodes;
            NodesCollectionView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(NodeViewModel.Name)));
            NodesCollectionView.SortDescriptions.Add(new SortDescription(nameof(NodeViewModel.Name), ListSortDirection.Ascending));

            }
        }

        void GetNodesRecursive(NodeViewModel thisViewModel, IEnumerable<INode> nodes, INode parent, int level = 0)
        {
            
            IEnumerable<INode> children = nodes.Where(x => x.ParentId == parent.Id);

            foreach (INode child in children)
            {
                    
                if (child.Type == NodeType.Directory)
                {
                    NodeViewModel _nextNodeViewModel = new NodeViewModel(child.Id, child.Name, false);
                    thisViewModel.Items.Add(_nextNodeViewModel);
                    GetNodesRecursive(_nextNodeViewModel, nodes, child, level + 1);
                       
                }
                else
                {
                    thisViewModel.Items.Add(new NodeViewModel(child.Id, child.Name, false));
                }

            }
        }

   

        private bool FilterNodes(object obj)
        {
            if (obj is NodeViewModel nodeViewModel)
            {
                return nodeViewModel.Name.Contains(NodesFilter, StringComparison.InvariantCultureIgnoreCase) ||
                    nodeViewModel.ItemId.Contains(NodesFilter, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

    }
}
