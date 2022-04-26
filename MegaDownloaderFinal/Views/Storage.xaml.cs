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
    /// Interaction logic for Storage.xaml
    /// </summary>
    public partial class Storage : UserControl
    {
        StorageModel sModel;
        public Storage()
        {
            InitializeComponent();
            sModel = new StorageModel();
            this.DataContext = sModel;
        }

        private void SelectFolderClick(object sender, RoutedEventArgs e)
        {
            RadOpenFolderDialog openFolderDialog = new RadOpenFolderDialog();
            openFolderDialog.ShowDialog();
            this.sModel.FolderName = openFolderDialog.FileName;
        }
    }
}
