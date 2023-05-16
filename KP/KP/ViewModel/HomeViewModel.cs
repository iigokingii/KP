using KP.db.context;
using KP.dbClasses;
using KP.DBMethods.UnitOfWork;
using KP.ViewModel.BaseModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Data.Entity;
using KP.db.dbClasses;
using Microsoft.VisualBasic.ApplicationServices;

namespace KP.ViewModel
{
    internal class HomeViewModel : ViewModelBase
    {
        public static string Login;
        UnitOfWork unit;
        DbAppContext db = new DbAppContext();
        private ObservableCollection<MiniItemInfo> _miniItemInfos;
        UserProfile user;
        private ObservableCollection<MiniItemInfo> _newCategory;
        private ObservableCollection<MiniItemInfo> _popularCategory;
        private ObservableCollection<MiniItemInfo> _GokinCategory;

        SolidColorBrush _foregraundOfLike;
        SolidColorBrush _foregraundOfDislike;
        SolidColorBrush _foregraundOfWatchLater;
        SolidColorBrush _foregraundOfDeleteFromWatchLater;
        SolidColorBrush _roz = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x47, 0xCA));
        SolidColorBrush _default = new SolidColorBrush(Color.FromArgb(0xFF, 0x94, 0x97, 0xCD));
        MiniItemInfo miniItem;




        Visibility _isVisible = Visibility.Visible;
        Visibility _isVisibleItem = Visibility.Collapsed;
        BigItemInfo _bigItemInfo;
        ObservableCollection<Review> _comments;


        string _title;
        string _titleOrig;
        BitmapImage _bigposter;
        string _year;
        string _genre;
        string _country;
        string _description;
        double _ratingIMDb;
        double _ratingKP;
        double _fees;
        string _directories;
        string _actors;
        ImageSource _frame1;
        ImageSource _frame2;
        ImageSource _frame3;
        ImageSource _frame4;
        string _comment;


        public ICommand ShowItemCommand { get; }
        public ICommand AddCommentByUserCommand { get; }
        public ICommand LikeFilmCommand { get; }
        public ICommand WatchLaterFilmCommand { get; }
        public ICommand DeleteLikeFilmCommand { get; }
        public ICommand DeleteWatchLaterFilmCommand { get; }

        public HomeViewModel()
        {

            unit = new UnitOfWork();
            ForegraundOfLike = _default;
            ForegraundOfDislike = _default;
            ForegraundOfDeleteFromWatchLater = _default;
            ForegraundOfWatchLater = _default;
            var t = unit.MiniItemInfoRepository.GetAll();
            LikeFilmCommand = new ViewModelCommandBase(LikeFilm);
            WatchLaterFilmCommand = new ViewModelCommandBase(WatchLaterFilm);
            DeleteLikeFilmCommand = new ViewModelCommandBase(DeleteLikeFilm);
            DeleteWatchLaterFilmCommand = new ViewModelCommandBase(DeleteWatchLaterFilm);
            if (t != null)
            {
                _miniItemInfos = new ObservableCollection<MiniItemInfo>(t.Select(p => p));
                _newCategory = new ObservableCollection<MiniItemInfo>(t.OrderBy(p=>p.Year).Take(6).Select(p=>p));
                _popularCategory = new ObservableCollection<MiniItemInfo>(t.Take(6).Select(p => p));
                _GokinCategory = new ObservableCollection<MiniItemInfo>(t.OrderByDescending(p => p.Year).Take(6).Select(p => p));
            }
            user = unit.Users.GetByLogin(Login);
            _comments = new ObservableCollection<Review>();
            ShowItemCommand = new ViewModelCommandBase(ShowItem);
            AddCommentByUserCommand = new ViewModelCommandBase(AddCommentByUser);
        }
        private void DeleteLikeFilm(object obj)
        {
            if (ForegraundOfDislike == _default)
            {
                ForegraundOfDislike = _roz;
                ForegraundOfLike = _default;
            }
            else
                ForegraundOfDislike = _default;

            Likes like = new Likes();
            like.userID = user.ID;
            like.bigItemInfoID = BigItemInfo.ID;
            like.miniItemInfoID = miniItem.ID;
            unit.LikesRepository.Remove(like);
            unit.Save();
        }

        private void DeleteWatchLaterFilm(object obj)
        {
            if (ForegraundOfDeleteFromWatchLater == _default)
            {
                ForegraundOfDeleteFromWatchLater = _roz;
                ForegraundOfWatchLater = _default;
            }
            else
                ForegraundOfWatchLater = _default;
            WatchLater later = new WatchLater();
            later.userID = user.ID;
            later.bigItemInfoID = BigItemInfo.ID;
            later.miniItemInfoID = miniItem.ID;
            unit.WatchLaterRepository.Remove(later);
            unit.Save();
        }

        //todo
        private void WatchLaterFilm(object obj)
        {
            if (ForegraundOfWatchLater == _default)
            {
                ForegraundOfWatchLater = _roz;
                ForegraundOfDeleteFromWatchLater = _default;
            }
            else
                ForegraundOfWatchLater = _default;
            WatchLater later = new WatchLater();
            later.userID = user.ID;
            later.bigItemInfoID = BigItemInfo.ID;
            later.miniItemInfoID = miniItem.ID;
            unit.WatchLaterRepository.Add(later);
            unit.Save();
        }
        //todo
        private void LikeFilm(object obj)
        {
            if (ForegraundOfLike == _default)
            {
                ForegraundOfLike = _roz;
                ForegraundOfDislike = _default;
            }
            else
                ForegraundOfLike = _default;

            Likes like = new Likes();
            like.userID = user.ID;
            like.bigItemInfoID = BigItemInfo.ID;
            like.miniItemInfoID = miniItem.ID;
            unit.LikesRepository.Add(like);
            unit.Save();
        }
        private void AddCommentByUser(object obj)
        {
            Review review = new Review();
            var user = unit.Users.GetByLogin(Thread.CurrentPrincipal.Identity.Name);
            review.Avatar = user.Avatar;
            review.bigItemInfo = _bigItemInfo;
            review.Date = DateTime.Now.ToShortDateString();
            review.userProfile = user;
            review.UserReviewText = Comment;
            Comment = "";
            review.Login = user.Login;
            unit.ReviewRepository.Add(review);
            Comments.Add(review);
            unit.Save();
        }
        private void ShowItem(object obj)
        {
            if (obj is MiniItemInfo)
            {
                miniItem = (MiniItemInfo)obj;
                _bigItemInfo = unit.BigItemInfoRepository.GetAll().First(p => p.ID == miniItem.ID);
            }
            IsVisibleItem = Visibility.Visible;
            IsVisible = Visibility.Collapsed;

            List<FramesFromMovie> frames = db.framesFromMovies.Include(c => c.BigItemInfo).Where(c => c.BigItemInfo == _bigItemInfo).ToList();
            var t = db.reviews.Include(c => c.bigItemInfo).Where(c => c.bigItemInfo == _bigItemInfo).ToList();

            Comments = new ObservableCollection<Review>(t.Select(p => p));

            switch (frames.Count)
            {
                case 1:
                    {
                        Frame1 = ConvertoBitmapImage(frames[0].Frame);
                        break;
                    }
                case 2:
                    {
                        Frame1 = ConvertoBitmapImage(frames[0].Frame);
                        Frame2 = ConvertoBitmapImage(frames[1].Frame);
                        break;
                    }
                case 3:
                    {
                        Frame1 = ConvertoBitmapImage(frames[0].Frame);
                        Frame2 = ConvertoBitmapImage(frames[1].Frame);
                        Frame3 = ConvertoBitmapImage(frames[2].Frame);
                        break;
                    }
                case 4:
                    {
                        Frame1 = ConvertoBitmapImage(frames[0].Frame);
                        Frame2 = ConvertoBitmapImage(frames[1].Frame);
                        Frame3 = ConvertoBitmapImage(frames[2].Frame);
                        Frame4 = ConvertoBitmapImage(frames[3].Frame);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            // byte[]->img
            byte[] d = (byte[])_bigItemInfo.BigImg;
            MemoryStream stream = new MemoryStream(d);
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = stream;
            bitmap.EndInit();

            BigPoster = bitmap;
            Title = BigItemInfo.Title;
            TitleOrig = BigItemInfo.TitleOrig;
            Year = BigItemInfo.Year;
            Genre = BigItemInfo.Genre;
            Country = BigItemInfo.Country;
            Description = _bigItemInfo.Description;
            RatingIMDb = BigItemInfo.ratingIMDb;
            RatingKP = BigItemInfo.ratingKP;
            Fees = BigItemInfo.fees;
            Directories = BigItemInfo.directors;
            Actors = BigItemInfo.actors;




            string tsda = "";

        }
        public SolidColorBrush ForegraundOfLike
        {
            get
            {
                return _foregraundOfLike;
            }
            set
            {
                _foregraundOfLike = value;
                OnPropertyChanged("ForegraundOfLike");
            }
        }
        public SolidColorBrush ForegraundOfDislike
        {
            get
            {
                return _foregraundOfDislike;
            }
            set
            {
                _foregraundOfDislike = value;
                OnPropertyChanged("ForegraundOfDislike");
            }
        }
        public SolidColorBrush ForegraundOfWatchLater
        {
            get
            {
                return _foregraundOfWatchLater;
            }
            set
            {
                _foregraundOfWatchLater = value;
                OnPropertyChanged("ForegraundOfWatchLater");
            }
        }
        public SolidColorBrush ForegraundOfDeleteFromWatchLater
        {
            get
            {
                return _foregraundOfDeleteFromWatchLater;
            }
            set
            {
                _foregraundOfDeleteFromWatchLater = value;
                OnPropertyChanged("ForegraundOfDeleteFromWatchLater");
            }
        }
        private ImageSource ConvertoBitmapImage(byte[]? frame)
        {
            MemoryStream stream = new MemoryStream(frame);
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = stream;
            bitmap.EndInit();
            return bitmap;
        }

        public ImageSource Frame4
        {
            get
            {
                return _frame4;
            }
            set
            {
                _frame4 = value;
                OnPropertyChanged("Frame4");
            }
        }
        public ImageSource Frame3
        {
            get
            {
                return _frame3;
            }
            set
            {
                _frame3 = value;
                OnPropertyChanged("Frame3");
            }
        }
        public ImageSource Frame2
        {
            get
            {
                return _frame2;
            }
            set
            {
                _frame2 = value;
                OnPropertyChanged("Frame2");
            }
        }
        public ImageSource Frame1
        {
            get
            {
                return _frame1;
            }
            set
            {
                _frame1 = value;
                OnPropertyChanged("Frame1");
            }
        }
        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
                OnPropertyChanged("Comment");
            }
        }
        public string Actors
        {
            get
            {
                return _actors;
            }
            set
            {
                _actors = value;
                OnPropertyChanged("Actors");
            }
        }
        public string Directories
        {
            get
            {
                return _directories;
            }
            set
            {
                _directories = value;
                OnPropertyChanged("Directories");
            }
        }
        public double Fees
        {
            get
            {
                return _fees;
            }
            set
            {
                _fees = value;
                OnPropertyChanged("Fees");
            }
        }
        public double RatingKP
        {
            get
            {
                return _ratingKP;
            }
            set
            {
                _ratingKP = value;
                OnPropertyChanged("RatingKP");
            }
        }
        public double RatingIMDb
        {
            get
            {
                return _ratingIMDb;
            }
            set
            {
                _ratingIMDb = value;
                OnPropertyChanged("RatingIMDb");
            }
        }
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
                OnPropertyChanged("Country");
            }
        }
        public string Genre
        {
            get
            {
                return _genre;
            }
            set
            {
                _genre = value;
                OnPropertyChanged("Genre");
            }
        }
        public string Year
        {
            get
            {
                return _year;
            }
            set
            {
                _year = value;
                OnPropertyChanged("Year");
            }
        }
        public string TitleOrig
        {
            get
            {
                return _titleOrig;
            }
            set
            {
                _titleOrig = value;
                OnPropertyChanged("TitleOrig");
            }
        }
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public ObservableCollection<Review> Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value;
                OnPropertyChanged("Comments");
            }
        }
        public ObservableCollection<MiniItemInfo> PopularCategory
        {
            get
            {
                return _popularCategory;
            }
            set
            {
                _popularCategory = value;
                OnPropertyChanged("PopularCategory");
            }
        }
        public ObservableCollection<MiniItemInfo> GokinCategory
        {
            get
            {
                return _GokinCategory;
            }
            set
            {
                _GokinCategory = value;
                OnPropertyChanged("GokinCategory");
            }
        }
        public ObservableCollection<MiniItemInfo> NewCategory
        {
            get
            {
                return _newCategory;
            }
            set
            {
                _newCategory = value;
                OnPropertyChanged("NewCategory");
            }
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
        public BigItemInfo BigItemInfo
        {
            get
            {
                return _bigItemInfo;
            }
            set
            {
                _bigItemInfo = value;
                OnPropertyChanged("IsVisibleItem");
            }
        }
        public BitmapImage BigPoster
        {
            get
            {
                return _bigposter;
            }
            set
            {
                _bigposter = value;
                OnPropertyChanged("BigPoster");
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
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
