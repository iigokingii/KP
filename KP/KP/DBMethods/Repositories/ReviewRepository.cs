using KP.db.context;
using KP.dbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KP.DBMethods.Repositories
{
    internal class ReviewRepository : IRepository<Review>
    {
        DbAppContext db;
        public ReviewRepository()
        {
            db = new DbAppContext();
        }
        public void Add(Review user)
        {
            db.reviews.Add(user);
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            throw new NotImplementedException();
        }

        public void Edit(Review user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Review> GetAll()
        {
            return db.reviews.ToList();
        }

        public Review GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Review GetByLogin(string login)
        {
            throw new NotImplementedException();
        }

        public void Remove(Review user)
        {
            db.reviews.Remove(user);
        }

        public void Save()
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex) { }

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
