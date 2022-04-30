using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Telerik.Windows.Controls;
using System.IO;
using CG.Web.MegaApiClient;
using System.Collections.Generic;
using System.Linq;

namespace MegaDownloaderFinal.ViewModels
{
    public class StorageModel : ViewModelBase
    {

        private readonly MegaApiClient client = new();
        private NodeListingViewModel nlvm = new();
        Defaults thisDefaults = new();


        private string? downloadMessage;
        public string DownloadMessage
        {
            get
            {
                if (downloadMessage == null)
                {
                    return "Welcome";
                }
                else 

                return this.downloadMessage;
            }
            set
            {
                if (this.downloadMessage != value)
                {
                    this.downloadMessage = value;
                    this.OnPropertyChanged(nameof(DownloadMessage));
                }
            }
        }
        private string? folderName;
        public string FolderName
        {
            get
            {
                if (folderName == null)
                    return "E:\\Megasync";
                else

                    return this.folderName;
            }
            set
            {
                if (this.folderName != value)
                {
                    this.folderName = value;
                    this.OnPropertyChanged(nameof(FolderName));
                }
            }
        }
        public void DownloadFolderLinkContents(NodesModel xnode, string dloadFolderName)
        {
            thisDefaults.SaveDirectory = dloadFolderName;

            INode parent = nlvm.nodes.Single(n => n.Id == xnode.ItemId);
            DownloadFolderLinkContents(parent);
        }


        void DownloadFolderLinkContents(INode parent)
        {
            
            if (!client.IsLoggedIn)
            { client.LoginAnonymous(); }

            List<String> files = new();

            if (parent.Type == NodeType.File)
            {
                string tempParent = parent.ParentId;
                while (tempParent != null)
                {
                    
                    INode parentnode = nlvm.nodes.Single(n => n.Id == tempParent);
                    files.Insert(0,parentnode.Name);
                    tempParent = parentnode.ParentId;

                }

                foreach (string folder in files)
                {
                    thisDefaults.SaveDirectory += @"\" + folder;
                    if (!Directory.Exists(thisDefaults.SaveDirectory))
                    {
                        Directory.CreateDirectory(thisDefaults.SaveDirectory);
                    }
                }

                
                thisDefaults.SaveDirectory = thisDefaults.SaveDirectory + @"\" + parent.Name;
                if (File.Exists(thisDefaults.SaveDirectory))
                {
                    File.Delete(thisDefaults.SaveDirectory);
                }
                client.DownloadFile(parent, thisDefaults.SaveDirectory);
                if ((DateTime?)parent.CreationDate != null)
                {
                    File.SetCreationTime(thisDefaults.SaveDirectory, (DateTime)parent.CreationDate);
                    File.SetLastWriteTime(thisDefaults.SaveDirectory, (DateTime)parent.CreationDate);
                }


            }


            if (client.IsLoggedIn)
            { client.Logout(); }

        }

        public class Defaults
        {
            public string? SaveDirectory { get; set; }
            public string? CurrentSaveDirectory { get; set; }
        }




    }
}

