using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MegaDownloaderFinal.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MegaNodesViewModel MegaNodesViewModel { get; }

        public MainViewModel()
        {
            MegaNodesViewModel = new MegaNodesViewModel();
        }
    }
}
