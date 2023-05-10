using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KP.dbClasses
{
    public class MiniItemInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public byte[] SmallIMG { get; set; }

        public BigItemInfo? BigItemInfo { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
