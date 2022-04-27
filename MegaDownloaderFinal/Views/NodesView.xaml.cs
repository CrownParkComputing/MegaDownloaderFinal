using System;
using System.IO;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MegaDownloaderFinal.ViewModels;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Input;
using Telerik.Windows.Controls.Navigation;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls.FileDialogs;
using Telerik.Windows.Data;
using CG.Web.MegaApiClient;
using System.Collections.Generic;
using System.Linq;

namespace MegaDownloaderFinal.Views
{
    /// <summary>
    /// Interaction logic for NodesListingView.xaml
    /// </summary>
    public partial class NodesView : UserControl
    {
        public int selectedItems;
        Defaults thisDefaults = new Defaults();
        StorageModel sm= new StorageModel();
        private readonly MegaApiClient client = new();

        public NodesView()
        {
            InitializeComponent();
        }


        private void SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                foreach (NodesModel i in e.AddedItems)
                {
                    if (i.Name != "WHDLoad")
                    {
                        if (i.Items.Count > 0)
                        {
                            foreach (NodesModel x in i.Items)
                            {
                                {
                                    //x.IsSelected = true;
                                    Nodes.SelectedItems.Add(x);
                                }
                            }
                        }
                    }
                }
            }

            if (e.RemovedItems.Count > 0)
            {
                foreach (NodesModel i in e.RemovedItems)
                {
                    if (i.Name != "WHDLoad")
                    {
                        if (i.Items.Count > 0)
                        {
                            foreach (NodesModel x in i.Items)
                            {
                                {
                                    //x.IsSelected = false;
                                    Nodes.SelectedItems.Remove(x);
                                }
                            }
                        }
                    }
                }
            }
        }




        public class Defaults
        {
            public string? SaveDirectory { get; set; }
            public string? CurrentSaveDirectory { get; set; }
        }

        private void BtnDownloadAll_Click(object sender, RoutedEventArgs e)
        {
            {
                client.LoginAnonymous();
                Uri folderLink = new Uri("https://mega.nz/folder/gdozjZxL#uI5SheetsAd-NYKMeRjf2A");
                IEnumerable<INode> nodes = client.GetNodesFromLink(folderLink); ;
                 
                thisDefaults.SaveDirectory = sm.FolderName;

                foreach (NodesModel i in this.Nodes.SelectedItems)
                {
                    if (i.Name != "WHDLoad")
                    {
                        if (i.Name.Contains("lha"))
                        {
                            INode node = nodes.Single(n => n.Id == i.ItemId);
                            thisDefaults.CurrentSaveDirectory = thisDefaults.SaveDirectory;
                            if (File.Exists(thisDefaults.SaveDirectory))
                            {
                                File.Delete(thisDefaults.SaveDirectory);
                            }

                            thisDefaults.SaveDirectory = thisDefaults.SaveDirectory + @"\" + node.Name;
                            client.DownloadFile(node, thisDefaults.SaveDirectory);
                            thisDefaults.SaveDirectory = thisDefaults.CurrentSaveDirectory;
                            File.SetCreationTime(thisDefaults.SaveDirectory + @"\" + node.Name, (DateTime)node.CreationDate);
                            File.SetLastWriteTime(thisDefaults.SaveDirectory + @"\" + node.Name, (DateTime)node.CreationDate);

                        }
                                            }
                }
                //foreach (NodesModel i in this.Nodes.SelectedItems)
                //{
                //    if (i.Name != "WHDLoad")
                //    {
                //        if (i.Name.Contains("lha"))
                //        {
                //            INode node = nodes.Single(n => n.Id == i.ItemId);
                //            thisDefaults.SaveDirectory = thisDefaults.SaveDirectory + @"\" + node.Name;

                //        }
                //    }
                //}

            }
        }
    }
}
