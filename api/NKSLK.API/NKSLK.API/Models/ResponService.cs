using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NKSLK.API.Models
{
    public class ResponService
    {
        public bool? IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
