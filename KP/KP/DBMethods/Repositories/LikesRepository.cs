using KP.db.context;
using KP.db.dbClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KP.DBMethods.Repositories
{
    internal class LikesRepository : IRepository<Likes>
    {
        DbAppContext db;
        public LikesRepository()
        {
            db = new DbAppContext();
        }
        public void Add(Likes user)
        {
            var likes = db.userLikes.ToList();
            bool here = false;
            foreach (var like in likes)
            {
                if (like.bigItemInfoID == user.bigItemInfoID && like.miniItemInfoID == user.miniItemInfoID && like.userID == user.userID)
                {
                    here = true;
                }
            }
            if (!here)
                db.userLikes.Add(user);
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

        public void Edit(Likes user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Likes> GetAll()
        {
            return db.userLikes.ToList();
        }

        public Likes GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Likes GetByLogin(string login)
        {
            throw new NotImplementedException();
        }

        public void Remove(Likes user)
        {
            Likes like = db.userLikes.FirstOrDefault(p=>p.miniItemInfoID==user.miniItemInfoID&&p.bigItemInfoID==user.bigItemInfoID&&p.userID==user.userID);
            if(like!=null)
                db.userLikes.Remove(like);
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
