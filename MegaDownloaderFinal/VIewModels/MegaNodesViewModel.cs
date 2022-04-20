using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;
using CG.Web.MegaApiClient;
using System.Linq;

namespace MegaDownloaderFinal.VIewModels
{
    public class MegaNodesViewModel : ViewModelBase
    {
        private readonly List<NodeViewModel> _nodeViewModel;
        private readonly MegaApiClient client = new();
        Uri folderLink = new Uri("https://mega.nz/folder/gdozjZxL#uI5SheetsAd-NYKMeRjf2A");

        public ICollectionView NodesCollectionView { get; }

        private string _nodesFilter = string.Empty;
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

        public MegaNodesViewModel()
        {
            IEnumerable<INode> nodes = new List<INode>();
            
            nodes = client.GetNodesFromLink(folderLink);
            _nodeViewModel = new List<NodeViewModel>();

            foreach (INode child in nodes)
            {
                _nodeViewModel.Add(new NodeViewModel(child.Name, child.Size.ToString(), (DateTime)child.CreationDate, child.Id, child.ParentId));
            }

            NodesCollectionView = CollectionViewSource.GetDefaultView(_nodeViewModel);

            NodesCollectionView.Filter = FilterNodes;
            NodesCollectionView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(NodeViewModel.ItemId)));
            NodesCollectionView.SortDescriptions.Add(new SortDescription(nameof(NodeViewModel.Name), ListSortDirection.Ascending));
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
