using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NKSLK.API.Models
{
    /// <summary>
    /// Model Đơn vị khoán
    /// </summary>
    public class UnitTasks
    {
        /// <summary>
        /// mã đơn vị khoán,key
        /// </summary>
        public int unittasks_id { get; set; }
        /// <summary>
        /// tên đơn vị khoán
        /// </summary>
        public string unittasks_name { get; set; }
    }
}
