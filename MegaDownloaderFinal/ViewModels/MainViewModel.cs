using System;
using System.Collections.Generic;
using System.Text;

namespace MegaDownloaderFinal.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public NodeListingViewModel NodeListingViewModel { get; }

        public MainViewModel()
        {
            NodeListingViewModel = new NodeListingViewModel();
        }
    }
}