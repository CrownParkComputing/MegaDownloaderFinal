using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;
using System.Linq;
using CG.Web.MegaApiClient;
using Telerik.Windows.Controls;
using System.IO;

namespace MegaDownloaderFinal.ViewModels
{
    public class NodesViewModel : ViewModelBase
    {
        
        private readonly MegaApiClient client = new MegaApiClient();
        public string? SaveDirectory { get; set; }
        public string? CurrentSaveDirectory { get; set; }

        public ICollectionView NodesCollectionView { get; }

        private string _nodesFilter = string.Empty;
        public INode root;
        public IEnumerable<INode> nodes { get; set; }


        NodesModel _nodeViewModel;

        ObservableCollection<NodesModel> _nodesCollection = new();
   

        public NodesViewModel()
        {
            {                    
                Uri folderLink = new Uri("https://mega.nz/folder/gdozjZxL#uI5SheetsAd-NYKMeRjf2A");
                client.LoginAnonymous();
                this.FolderName = Properties.Settings.Default.DownloadPath;
                nodes = client.GetNodesFromLink(folderLink);
                root = nodes.Single(n => n.Type == NodeType.Root);
                _nodeViewModel = new NodesModel(root.Id, root.Name, (DateTime)root.CreationDate);
                GetNodesRecursive(_nodeViewModel, nodes, root);

                _nodesCollection.Add(_nodeViewModel);


                NodesCollectionView = CollectionViewSource.GetDefaultView(_nodesCollection);

                NodesCollectionView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(NodesModel.Name)));
                NodesCollectionView.SortDescriptions.Add(new SortDescription(nameof(NodesModel.Name), ListSortDirection.Ascending));
                NodesCollectionView.Refresh();

            }
        }

        private string folderName;
        public string FolderName
        {
            get
            {
                if (string.IsNullOrEmpty(this.folderName))
                {
                    this.folderName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                }
                return this.folderName;
            }
            set
            {
                if (this.folderName != value)
                {
                    this.folderName = value;

                    Properties.Settings.Default.DownloadPath = value;
                    Properties.Settings.Default.Save();
                    this.OnPropertyChanged(nameof(FolderName));
                }
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

        public void DownloadFolderLinkContents(NodesModel thisItem)
        {

            if (!client.IsLoggedIn)
            { client.LoginAnonymous(); }

            List<String> files = new();
            INode parent = nodes.Single(x => x.Id == thisItem.ItemId);
            SaveDirectory = FolderName;
            if (parent.Type == NodeType.File)
            {
                string tempParent = parent.ParentId;
                while (tempParent != null)
                {

                    INode parentnode = nodes.Single(n => n.Id == tempParent);
                    files.Insert(0, parentnode.Name);
                    tempParent = parentnode.ParentId;

                }

                foreach (string folder in files)
                {
                    SaveDirectory += @"\" + folder;
                    if (!Directory.Exists(SaveDirectory))
                    {
                        Directory.CreateDirectory(SaveDirectory);
                    }
                }


                SaveDirectory = SaveDirectory + @"\" + parent.Name;
                if (File.Exists(SaveDirectory))
                {
                    File.Delete(SaveDirectory);
                }
                client.DownloadFile(parent, SaveDirectory);
                if ((DateTime?)parent.CreationDate != null)
                {
                    File.SetCreationTime(SaveDirectory, (DateTime)parent.CreationDate);
                    File.SetLastWriteTime(SaveDirectory, (DateTime)parent.CreationDate);
                }


            }

        }






    }
}
