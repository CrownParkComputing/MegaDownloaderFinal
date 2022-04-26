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

namespace MegaDownloaderFinal.Views
{
    /// <summary>
    /// Interaction logic for NodesListingView.xaml
    /// </summary>
    public partial class NodesView : UserControl
    {
        public int selectedItems;
        
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
                this.selectedCount.Text =  Nodes.SelectedItems.Count.ToString();

        }


    }
}
