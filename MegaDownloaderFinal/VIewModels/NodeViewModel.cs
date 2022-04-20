using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaDownloaderFinal.VIewModels
{
    public class NodeViewModel : ViewModelBase
    {
        private bool _isSelected;
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
         
        private string _size;
        public string Size
        {
            get
            {
                return _size;
            }
            private set
            {
                _size = value;
                OnPropertyChanged(nameof(Size));
            }
        }

        private DateTime _creationDate;
        public DateTime CreationDate
        {
            get
            {
                return _creationDate;
            }
            private set
            {
                _creationDate = value;
                OnPropertyChanged(nameof(CreationDate));
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

        private string _parentId;
        public string ParentId
        {
            get
            {
                return _parentId;
            }
            private set
            {
                _parentId = value;
                OnPropertyChanged(nameof(ParentId));
            }
        }

        public NodeViewModel(string name, string size, DateTime creationDate, string itemId, string parentId)
        {
            Name = name;
            Size = size;
            CreationDate = creationDate;
            ItemId = itemId;
            ParentId = parentId;
        }
    }
}
