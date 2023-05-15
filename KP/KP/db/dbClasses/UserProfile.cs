using KP.db.dbClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KP.dbClasses
{
    public class UserProfile
    {
        public UserProfile(int iD, string login, string email, byte[] avatar, string password)
        {
            ID = iD;
            Login = login;
            Email = email;
            Avatar = avatar;
            Password = password;
        }
        public UserProfile()
        {

        }

        public int ID { get; set; }
        public string Login { get; set; }
        public string? Email { get; set; }
        public byte[]? Avatar { get; set; }
        public string Password { get; set; }

        public List<Review> reviews { get; set; } = new();
        public List<Likes> likes { get; set; } = new();
        public List<WatchLater> watchLater { get; set; } = new();
    }
}
