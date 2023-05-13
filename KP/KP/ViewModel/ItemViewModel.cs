using KP.dbClasses;
using KP.ViewModel.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP.ViewModel
{
    internal class ItemViewModel:ViewModelBase
    {
        MiniItemInfo _bigItemInfo;

        private ViewModelBase _currChildView;

        public ItemViewModel()
        {

        }
        public ItemViewModel(MiniItemInfo bigItem)
        {
            _bigItemInfo = bigItem;
            _currChildView = new ItemViewModel();
        }





    }
}
