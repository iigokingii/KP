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
        public byte[]? Avatar { get; set; }
        public string Login { get; set; }
        public string UserReviewText { get; set; }
        public string Date { get; set; }

        public int? bigItemInfoID { get; set; }
        public BigItemInfo? bigItemInfo { get; set; }
        public int? userProfileID { get; set; }

        public UserProfile? userProfile { get; set; }
    }
}
