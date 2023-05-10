using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KP.dbClasses;
using KP.DBMethods.UnitOfWork;
using KP.ViewModel.BaseModels;

namespace KP.ViewModel
{
    public class ManePageViewModel:ViewModelBase
    {
        private UserProfile _currentUserProfile;
        UnitOfWork unit;

        public UserProfile CurrentUserProfile
        {
            get { return _currentUserProfile; }
            set
            {
                _currentUserProfile = value;
                OnPropertyChanged("CurrentUserProfile");

            }
        }
        public ManePageViewModel()
        {
            unit = new UnitOfWork();
            LoadCurrentUserData();
        }

        private void LoadCurrentUserData()
        {
            var user = unit.Users.GetByLogin(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserProfile = new UserProfile()
                {
                    
                };
            }
        }
    }
}
