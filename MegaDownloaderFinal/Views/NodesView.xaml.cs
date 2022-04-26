using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MegaDownloaderFinal.ViewModels;

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
                foreach (NodeViewModel i in e.AddedItems)
                {
                    if (i.Name != "WHDLoad")
                    {
                        if (i.Items.Count > 0)
                        {
                            foreach (NodeViewModel x in i.Items)
                            {
                                Nodes.SelectedItems.Add(x);
                            }
                        }
                    }
                }
            }
            if (this.Nodes != null)
                selectedItems =  this.Nodes.SelectedItems.Count;

        }
    }
}
