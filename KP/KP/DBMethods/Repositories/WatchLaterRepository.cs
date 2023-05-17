using KP.db.context;
using KP.db.dbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KP.DBMethods.Repositories
{
    internal class WatchLaterRepository : IRepository<WatchLater>
    {
        DbAppContext db;
        public WatchLaterRepository()
        {
            db = new DbAppContext();
        }
        public void Add(WatchLater user)
        {
            var watchlaters = db.userWatchLater.ToList();
            bool here = false;
            foreach(var later in watchlaters)
            {
                if (later.bigItemInfoID == user.bigItemInfoID && later.miniItemInfoID == user.miniItemInfoID && later.userID == user.userID)
                {
                    here = true;
                }
            }
            if (!here)
                db.userWatchLater.Add(user);
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            throw new NotImplementedException();
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

        public void Edit(WatchLater user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WatchLater> GetAll()
        {
            return db.userWatchLater.ToList();
        }

        public WatchLater GetById(int id)
        {
            throw new NotImplementedException();
        }

        public WatchLater GetByLogin(string login)
        {
            throw new NotImplementedException();
        }

        public void Remove(WatchLater user)
        {
            WatchLater later = db.userWatchLater.FirstOrDefault(p => p.miniItemInfoID == user.miniItemInfoID && p.bigItemInfoID == user.bigItemInfoID && p.userID == user.userID);
            if (later != null)
                db.userWatchLater.Remove(later);
        }

        public void Save()
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex) { }
        }
    }
}
