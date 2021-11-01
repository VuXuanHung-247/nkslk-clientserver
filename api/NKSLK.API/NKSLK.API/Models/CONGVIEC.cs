using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NKSLK.API.Models
{
    public class CONGVIEC
    {
        public int? ma_congviec { get; set; }
        public string tencongviec { get; set; }
        public int? dinhmuckhoan { get; set; }
        public int? ma_donvikhoan { get; set; }
        public double? hesokhoan { get; set; }
        public int? dinhmuclaodong { get; set; }
        public decimal? dongia { get; set; }

    }
}
