using System;
using System.IO;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MegaDownloaderFinal.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Threading.Tasks;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Input;
using Telerik.Windows.Controls.Navigation;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls.FileDialogs;

namespace MegaDownloaderFinal.Views
{
    /// <summary>
    /// Interaction logic for NodesListingView.xaml
    /// </summary>
    public partial class NodesView : UserControl
    {
        private NodesViewModel nvm = new NodesViewModel();
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
                                    Nodes.SelectedItems.Remove(x);
                                }
                            }
                        }
                    }
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


        private void RadProgressBar1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32((RadProgressBar1.Value / (RadProgressBar1.Maximum - RadProgressBar1.Minimum)) * 100);
            PercentageLabel.Content = value.ToString() + " %";
            if (this.RadProgressBar1.Value == this.RadProgressBar1.Maximum)
            {
                this.ButtonStart.IsEnabled = false;
                this.ButtonRestart.IsEnabled = true;
                this.GetFolderButton.IsEnabled = true;
                Nodes.IsBusy = false;

                this.LoadingLabel.Content = "Download Complete ";
            }
        }
        private void Start_Click(object sender, RoutedEventArgs e)  
        {
            RadProgressBar1.Maximum = Nodes.SelectedItems.Count;
            if (this.RadProgressBar1.Value < this.RadProgressBar1.Maximum)
            {
                this.Nodes.IsBusy = true;
                this.ButtonStart.IsEnabled = false;
                this.ButtonRestart.IsEnabled = false;
                this.GetFolderButton.IsEnabled = false;
            }
            Task.Factory.StartNew(() => DownloadSelected());

            
        }

        private void DownloadSelected()
        {

            foreach (NodesModel i in Nodes.SelectedItems)
            {
                if (i.Name.Contains("lha"))
                {

                    nvm.DownloadFolderLinkContents(i);
                }

                Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.LoadingLabel.Content = "Downloading : " + i.Name;
                    this.RadProgressBar1.Value += 1;
                }), DispatcherPriority.Background);
            }


        }
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            this.ButtonStart.IsEnabled = true;
            this.LoadingLabel.Content = "Downloading...";
            RadProgressBar1.Value = 0;
        }

        private void SelectFolderClick(object sender, RoutedEventArgs e)
        {
            RadOpenFolderDialog openFolderDialog = new RadOpenFolderDialog();
            openFolderDialog.ShowDialog();
            nvm.FolderName = openFolderDialog.FileName;
            this.DownloadFolder.Content = nvm.FolderName;

        }
    }
}
