using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NKSLK.API.Models
{
    public class Tasks
    {
        public int tasks_id { get; set; }//mã công việc
        public string tasks_name { get; set; }//tên công việc
        public decimal litmit_unit { get; set; }//định mức khoán
        public float limit_rate { get; set; }//hệ số khoán
        public int unittasks_id { get; set; }//mã đơn vị khoán
        public string unittasks_name { get; set; }//tên đơn vị khoán
        public decimal litmit_work { get; set; }//định mức lap động
        public decimal price { get; set; }//đơn giá
        public int quantityNKSLK { get; set; }//số lượng nhật ký sản lượng khoán

    }
}
