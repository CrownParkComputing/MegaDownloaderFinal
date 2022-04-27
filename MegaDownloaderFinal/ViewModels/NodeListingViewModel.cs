﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;
using System.Linq;
using CG.Web.MegaApiClient;
using Telerik.Windows.Controls;

namespace MegaDownloaderFinal.ViewModels
{
    public class NodeListingViewModel : ViewModelBase
    {
        
        private readonly MegaApiClient client = new();

        public ICollectionView NodesCollectionView { get; }

        private string _nodesFilter = string.Empty;
        public INode root;
        public IEnumerable<INode> nodes;


        NodesModel _nodeViewModel;

        ObservableCollection<NodesModel> _nodesCollection = new();
   

        public NodeListingViewModel()
        {
            {                    
                Uri folderLink = new Uri("https://mega.nz/folder/gdozjZxL#uI5SheetsAd-NYKMeRjf2A");
                client.LoginAnonymous();

                nodes = client.GetNodesFromLink(folderLink);
                root = nodes.Single(n => n.Type == NodeType.Root);
                _nodeViewModel = new NodesModel(root.Id, root.Name, (DateTime)root.CreationDate);
                GetNodesRecursive(_nodeViewModel, nodes, root);

                _nodesCollection.Add(_nodeViewModel);
                client.Logout(); 


                NodesCollectionView = CollectionViewSource.GetDefaultView(_nodesCollection);

                NodesCollectionView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(NodesModel.Name)));
                NodesCollectionView.SortDescriptions.Add(new SortDescription(nameof(NodesModel.Name), ListSortDirection.Ascending));
                NodesCollectionView.Refresh();

            }
        }

        void GetNodesRecursive(NodesModel thisViewModel, IEnumerable<INode> nodes, INode parent, int level = 0)
        {
            
            IEnumerable<INode> children = nodes.Where(x => x.ParentId == parent.Id);

            foreach (INode child in children)
            {
                NodesModel _nextNodeViewModel = new NodesModel(child.Id, child.Name, (DateTime)child.CreationDate);
                if (child.Type == NodeType.Directory)
                {
                    
                    thisViewModel.Items.Add(_nextNodeViewModel);
                    GetNodesRecursive(_nextNodeViewModel, nodes, child, level + 1);
                       
                }
                else
                {
                    thisViewModel.Items.Add(new NodesModel(child.Id, child.Name, (DateTime)child.CreationDate));
                }
            }
        }



   

    }
}
