using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KP.dbClasses
{
    public class Review
    {
        public int ID { get; set; }
        public byte[] Avatar { get; set; }
        public string Login { get; set; }
        public string UserReviewText { get; set; }

        public BigItemInfo? bigItemInfo { get; set; }
        public UserProfile? userProfile { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
