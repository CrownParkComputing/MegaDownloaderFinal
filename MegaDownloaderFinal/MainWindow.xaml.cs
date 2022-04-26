using MegaDownloaderFinal.Views;
using System.Windows;


namespace MegaDownloaderFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        
        
        public MainWindow()
        {
            InitializeComponent();
        }

        void DownloadSelected_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            NodesView x = this.a;
            count = x.selectedItems;
            this.selectedCount.Text = count.ToString();

        }
    }
}

