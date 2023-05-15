using KP.dbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP.db.dbClasses
{
    public class WatchLater
    {
        public int ID { get; set; }

        public int? userID { get; set; }
        public UserProfile user { get; set; }

        public int? bigItemInfoID { get; set; }
        public BigItemInfo bigItemInfo { get; set; }

        public int? miniItemInfoID { get; set; }
        public MiniItemInfo miniItemInfo { get; set; }
    }
}
