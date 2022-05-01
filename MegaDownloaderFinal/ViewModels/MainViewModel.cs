using System;
using System.Collections.Generic;
using System.Text;
using Telerik.Windows.Controls;

namespace MegaDownloaderFinal.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public NodesViewModel NodesViewModel { get; }

        public MainViewModel()
        {
            NodesViewModel = new NodesViewModel(); 
        }
    }
}