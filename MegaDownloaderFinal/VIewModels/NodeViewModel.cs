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
        private bool _isSelected;
        private bool isEmpty;
        private bool isExpanded;
        private ObservableCollection<NodeViewModel> items;


        public NodeViewModel(string nodeId, string name, bool empty)
        {
            this._itemId = nodeId;
            this.Name = name;
            this.isEmpty = empty;
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


        [Display(AutoGenerateField = false)]
        public bool IsExpanded
        {
            get
            {
                return this.isExpanded;
            }
            set
            {
                if (this.isExpanded != value)
                {
                    this.isExpanded = value;

                    this.LoadChildren();

                    OnPropertyChanged("IsExpanded");
                }
            }
        }

        [Display(AutoGenerateField = false)]
        public bool IsEmpty
        {
            get
            {
                return this.isEmpty;
            }
            set
            {
                this.isEmpty = value;
            }
        }

        [Display(AutoGenerateField = false)]
        public ObservableCollection<NodeViewModel> Items { get; set; }



        public void LoadChildren()
        {
            //if (this.nodes == null)
            //{
            //    this.nodes = new ObservableCollection<NodeViewModel>(from f in this.nodeElement.Elements("node")
            //                                                           select new NodeViewModel(f)
            //                                                           {
            //                                                               Name = f.Attribute("Name").Value,
            //                                                               IsEmpty = bool.Parse(f.Attribute("IsEmpty").Value),
            //                                                               CreationDate = DateTime.Parse(f.Attribute("CreationDate").Value, System.Globalization.CultureInfo.InvariantCulture),
            //                                                           });
            //    this.OnPropertyChanged("Items");
            //}
        }
    }
}
