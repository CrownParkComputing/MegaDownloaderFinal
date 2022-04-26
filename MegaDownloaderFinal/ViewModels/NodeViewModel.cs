using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using Telerik.Windows.Controls;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using CG.Web.MegaApiClient;

namespace MegaDownloaderFinal.ViewModels
{
    public class NodeViewModel : ViewModelBase
    {
        private bool isExpanded;
        private string name;
        private int count;
        private bool _isSelected;
        private ObservableCollection<NodeViewModel> items;


        public NodeViewModel(string nodeId, string name, DateTime createdDate, bool isExpanded = true)
        {
            this._itemId = nodeId;
            this.Name = name;
            this.CreatedDate = (DateTime)createdDate;
            this.Items = new ObservableCollection<NodeViewModel>();

        }



        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            private set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _itemId;
        public string ItemId
        {
            get
            {
                return _itemId;
            }
            private set
            {
                _itemId = value;
                OnPropertyChanged(nameof(ItemId));
            }
        }

        private DateTime _createdDate;
        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }
            private set
            {
                _createdDate = value;
                OnPropertyChanged(nameof(CreatedDate));
            }
        }

        public bool IsExpanded
        {
            get
            {
                return this.isExpanded;
            }
            set
            {
                if (value != this.isExpanded)
                {
                    this.isExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }
            }
        }
        public int Count
        {
            get
            {
                return this.count;
            }
            set
            {
                if (value != this.count)
                {
                    this.count = value;
                    this.OnPropertyChanged("Count");
                }
            }
        }


        [Display(AutoGenerateField = false)]
        public ObservableCollection<NodeViewModel> Items { get; set; }

    }
}