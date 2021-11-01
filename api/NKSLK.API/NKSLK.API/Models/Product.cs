using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NKSLK.API.Models
{
    public class Product
    {
        public int ma_sanpham { get; set; }
        public string tensanpham { get; set; }
        public int sodangky { get; set; }
        public DateTime hansudung { get; set; }
        public string quycach { get; set; }
        public DateTime ngaydangky { get; set; }
    }
}
