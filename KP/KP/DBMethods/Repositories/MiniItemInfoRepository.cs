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
    internal class MiniItemInfoRepository : IRepository<MiniItemInfo>
    {
        private DbAppContext db;
        public MiniItemInfoRepository()
        {
            db = new DbAppContext();
        }
        public int GetUserId(MiniItemInfo user)
        {
            MiniItemInfo us = db.miniItemInfos.FirstOrDefault(p => user.Year == p.Year && user.Name == p.Name && user.Genre == p.Genre);
            if(us!=null)
                return us.ID;
            return -1;
        }

        public void Add(MiniItemInfo user)
        {
            db.miniItemInfos.Add(user);
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            throw new NotImplementedException();
        }

        public void Edit(MiniItemInfo user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MiniItemInfo> GetAll()
        {
            return db.miniItemInfos.ToList();
        }

        public MiniItemInfo GetById(int id)
        {
            return db.miniItemInfos.FirstOrDefault(p => p.ID.Equals(id));
        }

        public MiniItemInfo GetByLogin(string login)
        {
            throw new NotImplementedException();
        }

        public void Remove(MiniItemInfo user)
        {
            db.miniItemInfos.Remove(user);
        }

        public void Save()
        {
            try
            {
                db.SaveChanges();

            }
            catch (Exception e) { }
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
