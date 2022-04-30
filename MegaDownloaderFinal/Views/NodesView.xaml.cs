using System;
using System.IO;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MegaDownloaderFinal.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MegaDownloaderFinal.Views
{
    /// <summary>
    /// Interaction logic for NodesListingView.xaml
    /// </summary>
    public partial class NodesView : UserControl
    {
        StorageModel sm = new StorageModel();


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
                    else
                    {

                        Nodes.SelectedItems.Remove(i);

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

    private void BtnDownloadAll_Click(object sender, RoutedEventArgs e)
        {

            foreach (NodesModel i in Nodes.SelectedItems)
            {
            if (i.Name.Contains("lha"))
                {
                    
                    sm.DownloadMessage = "Currently Downloading : " + i.Name;

                    // how do I get sm.FolderName to be passed here or called from with StorageModel its always null ??
                    sm.DownloadFolderLinkContents(i, "E:\\Megasync");
                }
            }

        }

        private void Nodes_Filtered(object sender, Telerik.Windows.Controls.GridView.GridViewFilteredEventArgs e)
        {

             
            foreach (NodesModel x in Nodes.Items)
            {
                {
                    if (x.Name.Contains("lha"))
                    { 
                        Nodes.SelectedItems.Add(x);
                    }
                    if (x.Name == "WHDLoad")
                    {
                        Nodes.SelectedItems.Remove(x);
                    }
                }
            }
        }      

       
    }
}
