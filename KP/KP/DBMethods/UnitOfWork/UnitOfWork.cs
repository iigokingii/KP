using KP.db.context;
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
        


        public UserProfileRepository Users
        {
            get
            {
                if(_userProfileReposit == null)
                    _userProfileReposit = new UserProfileRepository();
                return _userProfileReposit;
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
