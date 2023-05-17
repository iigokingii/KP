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
            db.bigItemInfoInfos.Add(user);
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            throw new NotImplementedException();
        }
        public void Edit(BigItemInfo user)
        {
            BigItemInfo itemInfo= db.bigItemInfoInfos.ToList().ElementAt(user.ID-1);
            db.bigItemInfoInfos.Remove(user);
            itemInfo.ID=user.ID;
            itemInfo.Title = user.Title;
            itemInfo.TitleOrig = user.TitleOrig;
            itemInfo.BigImg = user.BigImg;
            itemInfo.actors = user.actors;
            itemInfo.Country = user.Country;
            itemInfo.fees = user.fees;
            itemInfo.Description = user.Description;
            itemInfo.directors= user.directors;
            itemInfo.framesFromMovies = user.framesFromMovies;
            itemInfo.Genre = user.Genre;
            itemInfo.MiniItemInfo = user.MiniItemInfo;
            //itemInfo.MiniItemInfoId = user.MiniItemInfoId;
            itemInfo.Year = user.Year;
            itemInfo.reviews = user.reviews;
            itemInfo.ratingKP = user.ratingKP;
            itemInfo.ratingIMDb = user.ratingIMDb;
            db.bigItemInfoInfos.Add(itemInfo);
            
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
            db.bigItemInfoInfos.Remove(user);
        }

        public void Save()
        {
            /*try
            {*/
                db.SaveChanges();
            /*}
            catch (Exception ex) { }*/

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
