using FontAwesome.Sharp;
using KP.dbClasses;
using KP.DBMethods.UnitOfWork;
using KP.View;
using KP.ViewModel.BaseModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KP.ViewModel
{
    internal class CatalogViewModel:ViewModelBase
    {
        UnitOfWork unit;
        private ObservableCollection<MiniItemInfo> _miniItemInfos;
        Visibility _isVisible = Visibility.Visible;
        Visibility _isVisibleItem = Visibility.Collapsed;
        public ICommand testcom { get; }
        public CatalogViewModel()
        {
            
            unit = new UnitOfWork();
            var t = unit.MiniItemInfoRepository.GetAll();
            if (t != null)
                _miniItemInfos = new ObservableCollection<MiniItemInfo>(t.Select(p => p));
            testcom = new ViewModelCommandBase(test);
        }

        private void test(object obj)
        {
            BigItemInfo item;
            if (obj is MiniItemInfo)
            {
                MiniItemInfo temp = (MiniItemInfo)obj;
                item = unit.BigItemInfoRepository.GetAll().First(p => p.ID == temp.ID);
            }
            IsVisibleItem = Visibility.Visible;
            IsVisible = Visibility.Collapsed;
        }
        public ObservableCollection<MiniItemInfo> MiniItemInfos
        {
            get
            {
                return _miniItemInfos;
            }
            set
            {
                _miniItemInfos = value;
                OnPropertyChanged("MiniItemInfos");
            }
        }
        public Visibility IsVisibleItem
        {
            get
            {
                return _isVisibleItem;
            }
            set
            {
                _isVisibleItem = value;
                OnPropertyChanged("IsVisibleItem");
            }
        }
        public Visibility IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }
    }
}
