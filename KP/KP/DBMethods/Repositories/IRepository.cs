using KP.dbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KP.DBMethods.Repositories
{
    public interface IRepository<T>:IDisposable
        where T : class
    {
        bool AuthenticateUser(NetworkCredential credential);
        void Add(T user);
        void Edit(T user);
        void Remove(T user);
        T GetById(int id);
        T GetByLogin(string login);
        IEnumerable<T> GetAll();
        void Save();
    }
}
