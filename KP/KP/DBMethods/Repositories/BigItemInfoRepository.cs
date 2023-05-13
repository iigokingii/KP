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
    internal class BigItemInfoRepository : IRepository<BigItemInfo>
    {
        private DbAppContext db;
        public BigItemInfoRepository()
        {
            db = new DbAppContext();
        }
        public void Add(BigItemInfo user)
        {
            db.Add(user);
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            throw new NotImplementedException();
        }
        public void Edit(BigItemInfo user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BigItemInfo> GetAll()
        {
            return db.bigItemInfoInfos.ToList();
        }

        public BigItemInfo GetById(int id)
        {
            return db.bigItemInfoInfos.FirstOrDefault(p => p.ID.Equals(id));
        }

        public BigItemInfo GetByLogin(string login)
        {
            throw new NotImplementedException();
        }

        public void Remove(BigItemInfo user)
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
