using System;
using System.Collections.Generic;
using System.Text;
using Telerik.Windows.Controls;

namespace MegaDownloaderFinal.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public NodeListingViewModel NodeListingViewModel { get; }
        public StorageModel StorageModel { get; }

        public MainViewModel()
        {
            NodeListingViewModel = new NodeListingViewModel();
            StorageModel = new StorageModel();  
        }
    }
}