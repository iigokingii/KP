using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FontAwesome.Sharp;
using KP.dbClasses;
using KP.DBMethods.UnitOfWork;
using KP.View;
using KP.ViewModel.BaseModels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace KP.ViewModel
{
    public class ManePageViewModel:ViewModelBase
    {
        UnitOfWork unit;
        private UserProfile _currentUserProfile;
        private string ErrorMsg;

        private ViewModelBase _currChildView;
        private string _captionOfHeader;
        private IconChar _iconOfHeader;

        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowCatalogViewCommand { get; }
        public ICommand ShowAddViewCommand { get; }


        public ManePageViewModel()
        {
            unit = new UnitOfWork();
            LoadCurrentUserData();
            ShowHomeViewCommand = new ViewModelCommandBase(ShowHome);
            ShowCatalogViewCommand = new ViewModelCommandBase(ShowCatalog);
            ShowAddViewCommand = new ViewModelCommandBase(ShowAdd);
            ShowHome(null);

        }

        private void ShowAdd(object obj)
        {
            CurrentChildView = new AddViewModel();
            CaptionOfHeader = "Add";
            IconOfHeader = IconChar.Add;

        }

        private void ShowCatalog(object obj)
        {
            CurrentChildView = new CatalogViewModel();
            CaptionOfHeader = "Catalog";
            IconOfHeader = IconChar.Ticket;
        }

        private void ShowHome(object obj)
        {
            CurrentChildView = new HomeViewModel();
            CaptionOfHeader = "Home";
            IconOfHeader = IconChar.Home;
        }


        public ViewModelBase CurrentChildView
        {
            get { return _currChildView; }
            set
            {
                _currChildView = value;
                OnPropertyChanged("CurrentChildView");

            }
        }
        public string CaptionOfHeader
        {
            get { return _captionOfHeader; }
            set
            {
                _captionOfHeader = value;
                OnPropertyChanged("CaptionOfHeader");

            }
        }
        public IconChar IconOfHeader
        {
            get { return _iconOfHeader; }
            set
            {
                _iconOfHeader = value;
                OnPropertyChanged("IconOfHeader");
            }
        }
        public UserProfile CurrentUserProfile
        {
            get { return _currentUserProfile; }
            set
            {
                _currentUserProfile = value;
                OnPropertyChanged("CurrentUserProfile");

            }
        }



        private void LoadCurrentUserData()
        {

            Thread.CurrentPrincipal = new GenericPrincipal(
                   new GenericIdentity("admin"), null);

            var user = unit.Users.GetByLogin(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserProfile = new UserProfile()
                {
                    Login = user.Login,
                    ID = user.ID,
                    Avatar = user.Avatar,
                    reviews = user.reviews,
                    Email = user.Email
                };
                
            }
            else
            {
                ErrorMsg = "Пользователь не вошел в аккаунт";
                Thread.Sleep(1000);
                Application.Current.Shutdown();
            }

        }
    }
}
