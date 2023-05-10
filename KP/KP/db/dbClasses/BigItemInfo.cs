using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KP.dbClasses
{
    public class BigItemInfo
    {
        public int ID { get; set; }
         public byte[] BigImg { get; set; }
        public string Year { get; set; }
        public string Genre { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }


        public List<FramesFromMovie> framesFromMovies { get; set; } = new();
        public List<Review> reviews { get; set; } = new();
        public int MiniItemInfoId { get; set; }
        public MiniItemInfo? MiniItemInfo { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
