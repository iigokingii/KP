using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KP.dbClasses
{
    public class FramesFromMovie
    {
        public int ID { get; set; }
        public byte[]? Frame { get; set; }

        public int? BigItemInfoID { get; set; }
        public BigItemInfo? BigItemInfo { get; set; }
    }
}
