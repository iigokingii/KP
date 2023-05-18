using FontAwesome.Sharp;
using KP.dbClasses;
using KP.DBMethods.UnitOfWork;
using KP.ViewModel.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace KP.ViewModel
{
    internal class MainWindowUserViewModel:ViewModelBase
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
        public ICommand ShowUserProfileCommand { get; }
        public ICommand ShowSettingsCommand { get; }
        public ICommand ShowFilmsCommand { get; }
        public ICommand ShowLikedFilmsCommand { get; }
        public ICommand ShowWatchLagerFilmsCommand { get; }
        public MainWindowUserViewModel()
        {
            unit = new UnitOfWork();

            ShowHomeViewCommand = new ViewModelCommandBase(ShowHome);
            ShowCatalogViewCommand = new ViewModelCommandBase(ShowCatalog);
           
            ShowUserProfileCommand = new ViewModelCommandBase(ShowUserProfile);
            ShowSettingsCommand = new ViewModelCommandBase(ShowSettings);
            ShowFilmsCommand = new ViewModelCommandBase(ShowFilms);
            ShowLikedFilmsCommand = new ViewModelCommandBase(ShowLikedFilms);
            ShowWatchLagerFilmsCommand = new ViewModelCommandBase(ShowWatchLagerFilms);
            LoadCurrentUserData();
            ShowHome(null);
        }
        private void ShowWatchLagerFilms(object obj)
        {
            CaptionOfHeader = "WatchLater";
            IconOfHeader = IconChar.Clock;
            WatchLaterViewModel.Login = _currentUserProfile.Login;
            CurrentChildView = new WatchLaterViewModel();
        }

        private void ShowLikedFilms(object obj)
        {
            CaptionOfHeader = "Liked";
            IconOfHeader = IconChar.ThumbsUp;
            LikesViewModel.Login = _currentUserProfile.Login;
            CurrentChildView = new LikesViewModel();
        }

        private void ShowFilms(object obj)
        {
            FilmsViewModel.Login = _currentUserProfile.Login;
            CurrentChildView = new FilmsViewModel();
            CaptionOfHeader = "Films";
            IconOfHeader = IconChar.Film;
        }

        private void ShowSettings(object obj)
        {
            CurrentChildView = new SettingViewModel();

            CaptionOfHeader = "Settings";
            IconOfHeader = IconChar.Gear;
        }

        private void ShowUserProfile(object obj)
        {
            Thread.CurrentPrincipal = new GenericPrincipal(
                   new GenericIdentity(_currentUserProfile.Login), null);
            CaptionOfHeader = "Profile";
            IconOfHeader = IconChar.Sliders;
            CurrentChildView = new UserProfileViewModel();
        }

        private void ShowCatalog(object obj)
        {

            CurrentChildView = new CatalogViewModel();
            Thread.CurrentPrincipal = new GenericPrincipal(
       new GenericIdentity(_currentUserProfile.Login), null);
            CaptionOfHeader = "Catalog";
            IconOfHeader = IconChar.Ticket;
        }

        private void ShowHome(object obj)
        {
            HomeViewModel.Login = CurrentUserProfile.Login;
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

            /*Thread.CurrentPrincipal = new GenericPrincipal(
                   new GenericIdentity("admin"), null);*/
            try
            {
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
                    MessageBox.Show("Пользователь не вошел в аккаунт", "log in", MessageBoxButton.OK);
                    Thread.Sleep(1000);
                    Application.Current.Shutdown();
                }
            }
            catch (Exception ex)
            {
                
            }

        }
    }
}
