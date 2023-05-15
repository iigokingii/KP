using KP.db.context;
using KP.DBMethods.Repositories;
using KP.DBMethods.Repositories.UserProfileRepositor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP.DBMethods.UnitOfWork
{
    internal class UnitOfWork : IDisposable
    {
        DbAppContext db = new DbAppContext();
        private UserProfileRepository _userProfileReposit;
        private MiniItemInfoRepository _miniItemInfoRepository;
        private FramesFromMovieRepository _framesFromMovieRepository;
        private BigItemInfoRepository _bigItemInfoRepository;
        private ReviewRepository _reviewRepository;
        private LikesRepository _likesRepository;
        private WatchLaterRepository _watchLaterRepository;
        public WatchLaterRepository WatchLaterRepository
        {
            get
            {
                if (_watchLaterRepository == null)
                    _watchLaterRepository = new WatchLaterRepository();
                return _watchLaterRepository;
            }
        }
        public LikesRepository LikesRepository
        {
            get
            {
                if (_likesRepository == null)
                    _likesRepository = new LikesRepository();
                return _likesRepository;
            }
        }
        public ReviewRepository ReviewRepository
        {
            get
            {
                if (_reviewRepository == null)
                    _reviewRepository = new ReviewRepository();
                return _reviewRepository;
            }
        }
        public BigItemInfoRepository BigItemInfoRepository
        {
            get
            {
                if (_bigItemInfoRepository == null)
                    _bigItemInfoRepository = new BigItemInfoRepository();
                return _bigItemInfoRepository;
            }
        }
        public UserProfileRepository Users
        {
            get
            {
                if(_userProfileReposit == null)
                    _userProfileReposit = new UserProfileRepository();
                return _userProfileReposit;
            }
        }
        public MiniItemInfoRepository MiniItemInfoRepository
        {
            get
            {
                if (_miniItemInfoRepository == null)
                    _miniItemInfoRepository = new MiniItemInfoRepository();
                return _miniItemInfoRepository;
            }
        }
        public FramesFromMovieRepository FramesFromMovieRepository
        {
            get
            {
                if (_framesFromMovieRepository == null)
                    _framesFromMovieRepository = new FramesFromMovieRepository();
                return _framesFromMovieRepository;
            }
        }

        public void Save()
        {
            BigItemInfoRepository.Save();
            Users.Save();
            MiniItemInfoRepository.Save();
            FramesFromMovieRepository.Save();
            ReviewRepository.Save();
            WatchLaterRepository.Save();
            LikesRepository.Save();
        }


        private bool disponsed = false;
        public virtual void Disponse(bool disposing)
        {
            if (!this.disponsed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disponsed = true;
        }
        public void Dispose()
        {
            Disponse(true);
            GC.SuppressFinalize(this);
        }
    }
}
