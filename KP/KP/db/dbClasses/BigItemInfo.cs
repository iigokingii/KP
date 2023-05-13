using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KP.dbClasses
{
    public class BigItemInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Title { get; set; }
        public string TitleOrig { get; set; }
        public byte[]? BigImg { get; set; }
        public string Year { get; set; }
        public string Genre { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public double ratingIMDb { get; set; }
        public double ratingKP { get; set; }
        public double fees { get; set; }
        public string directors { get; set; }
        public string actors { get; set; }



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
