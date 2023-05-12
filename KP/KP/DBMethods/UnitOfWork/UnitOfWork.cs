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
