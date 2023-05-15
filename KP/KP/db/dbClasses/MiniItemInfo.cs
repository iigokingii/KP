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
    public class MiniItemInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
        public byte[]? SmallIMG { get; set; }

        public BigItemInfo? BigItemInfo { get; set; }
    }
}
