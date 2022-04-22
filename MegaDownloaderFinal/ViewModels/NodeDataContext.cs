﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Telerik.Windows.Controls;
using System.ComponentModel.DataAnnotations;
using CG.Web.MegaApiClient;

namespace MegaDownloaderFinal.ViewModels
{
    public class NodeDataContext : ViewModelBase
    {
        
        private readonly MegaApiClient client = new();


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
                //NodesCollectionView.Refresh();
            }
        }
        

        ObservableCollection<NodeViewModel>? nodesCollection;
        public ObservableCollection<NodeViewModel> NodesCollection
        {
            get
            {
                if (nodesCollection == null)
                {
                    nodesCollection = new ObservableCollection<NodeViewModel>();
                    Uri folderLink = new Uri("https://mega.nz/folder/gdozjZxL#uI5SheetsAd-NYKMeRjf2A");
                    client.LoginAnonymous();
                    IEnumerable<INode> nodes = client.GetNodesFromLink(folderLink);
                    INode parent = nodes.Single(n => n.Type == NodeType.Root);

                    var parents = new NodeViewModel(parent.Id, parent.Name, false);
                    parents.Items.Add(new NodeViewModel(parent.Id, parent.Name, false));
                    var newparent2 = new NodeViewModel(parent.Id, parent.Name, true);
                    newparent2.Items.Add(new NodeViewModel(parent.Id, parent.Name, false));
                    parents.Items.Add(newparent2);
                    nodesCollection.Add(parents);

                    //GetNodesRecursive(nodes, parent);
                    client.Logout(); 
                }
                
                return nodesCollection;

            }
        }

        void GetNodesRecursive(IEnumerable<INode> nodes, INode parent, int level = 0)
        {
            NodeViewModel newparent = new(parent.Id, parent.Name, false);
            //nodesCollection.Add(newparent);
            IEnumerable<INode> children = nodes.Where(x => x.ParentId == parent.Id);
            
            foreach (INode child in children)
            {
                if (child.Type == NodeType.Directory)
                {
                    
                    GetNodesRecursive(nodes, child, level + 1);
                }
            }
        }

        //private List<NodeViewModel> GetNodesFromParentID(IEnumerable<INode> nodes, INode parent)
        //{
        //    List<NodeViewModel> tempView = new List<NodeViewModel>();
        //    IEnumerable<INode> children = nodes.Where(x => x.ParentId == parent.Id);
        //    foreach (INode child in children)
        //    {
        //        tempView.Add(new NodeViewModel(child.Name, child.Size.ToString(), (DateTime)child.CreationDate, child.Id, child.ParentId));
        //    }

        //    return tempView;
        //}

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
