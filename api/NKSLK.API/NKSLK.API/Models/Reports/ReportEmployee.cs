using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NKSLK.API.Models.Reports
{
    public class ReportEmployee
    {
        public int ma_nkslk { get; set; }
        public int ma_congnhan { get; set; }
        public string hoten { get; set; }
        public DateTime? ngaybatdau { get; set; }
        public DateTime? thoigian_batdau { get; set; }
        public DateTime? thoigian_ketthuc { get; set; }
    }
}
