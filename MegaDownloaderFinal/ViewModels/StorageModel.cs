using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace MegaDownloaderFinal.ViewModels
{
    public class StorageModel : ViewModelBase
    { 
        
        
        private string folderName;
        public string FolderName
        {
            get
            {
                if (string.IsNullOrEmpty(this.folderName))
                {
                    this.folderName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString();
                }
                return this.folderName;
            }
            set
            {
                if (this.folderName != value)
                {
                    this.folderName = value.ToString();
                    this.OnPropertyChanged("FolderName");
                }
            }
        }
    }
}
