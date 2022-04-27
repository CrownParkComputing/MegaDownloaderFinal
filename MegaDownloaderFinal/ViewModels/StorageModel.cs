using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace MegaDownloaderFinal.ViewModels
{
    public class StorageModel : ViewModelBase
    {


        private string folderName = "E:/Megasync";
        public string FolderName
        {
            get
            {
                if (string.IsNullOrEmpty(this.folderName))
                {
                    this.folderName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                }
                return this.folderName;
            }
            set
            {
                if (this.folderName != value)
                {
                    this.folderName = value;
                    this.OnPropertyChanged("FolderName");
                }
            }
        }
    }
}
