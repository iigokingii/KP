using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using KP.db.context;
using KP.dbClasses;
using KP.View.login;

namespace KP.DBMethods.Repositories.UserProfileRepositor
{
    internal class UserProfileRepository : IRepository<UserProfile>
    {
        private DbAppContext db;
        public UserProfileRepository()
        {
            this.db = new DbAppContext();
        }

        public void Add(UserProfile user)
        {
            db.userProfiles.Add(user);
            this.Save();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            UserProfile? us =  db.userProfiles.FirstOrDefault(p => p.Login == credential.UserName && p.Password == credential.Password);
            validUser = us==null? false : true;
            return validUser;
        }
        public void Edit(UserProfile _user)
        {
            UserProfile user= db.userProfiles.FirstOrDefault(p => p.Login == _user.Login && p.Email == _user.Email && p.ID == _user.ID);
            user.reviews = _user.reviews;
            user.Email = _user.Email;
            user.Avatar = _user.Avatar;
            user.ID = _user.ID;
            user.Login= _user.Login;
            this.Save();
        }

        public IEnumerable<UserProfile> GetAll()
        {
            return db.userProfiles.ToList();
        }


        /////////////TODO
        public UserProfile GetById(int id)
        {
            return db.userProfiles.FirstOrDefault(p => p.ID == id);
        }

        public UserProfile GetByLogin(string login)
        {
            return db.userProfiles.FirstOrDefault(p => p.Login == login);
        }
        //////////////////
        

        public void Remove(UserProfile user)
        {
            db.userProfiles.Remove(user);
            this.Save();
        }
        public void Save()
        {
            this.db.SaveChanges();
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
