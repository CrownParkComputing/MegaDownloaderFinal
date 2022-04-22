using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MegaDownloaderFinal.ViewModels;
using System.Windows;

namespace MegaDownloaderFinal
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new NodeDataContext()
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
