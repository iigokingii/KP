using KP.dbClasses;
using KP.DBMethods.UnitOfWork;
using KP.ViewModel.BaseModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP.ViewModel
{
    internal class CatalogViewModel:ViewModelBase
    {
        UnitOfWork unit;
        private ObservableCollection<MiniItemInfo> _miniItemInfos;
        public CatalogViewModel()
        {
            unit = new UnitOfWork();
            var t = unit.MiniItemInfoRepository.GetAll();
            if (t != null)
                _miniItemInfos = new ObservableCollection<MiniItemInfo>(t.Select(p => p));
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
    }
}
