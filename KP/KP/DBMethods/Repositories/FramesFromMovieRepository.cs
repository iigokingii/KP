using KP.db.context;
using KP.dbClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KP.DBMethods.Repositories
{
    internal class FramesFromMovieRepository : IRepository<FramesFromMovie>
    {
        private DbAppContext db;
        public FramesFromMovieRepository()
        {
            db = new DbAppContext();
        }
        public void Add(FramesFromMovie user)
        {
            db.Add(user);
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            throw new NotImplementedException();
        }

        
        public void Edit(FramesFromMovie user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FramesFromMovie> GetAll()
        {
            return db.framesFromMovies.ToList();
        }

        public FramesFromMovie GetById(int id)
        {
            return db.framesFromMovies.FirstOrDefault(p => p.ID.Equals(id));
        }

        public FramesFromMovie GetByLogin(string login)
        {
            throw new NotImplementedException();
        }

        public void Remove(FramesFromMovie user)
        {
            db.Remove(user);
        }

        public void Save()
        {
            db.SaveChanges();
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
