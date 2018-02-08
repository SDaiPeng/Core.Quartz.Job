using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quartz.Admin.Models
{
    public class ApiResult<T>
    {
        public bool IsSuccess { get; set; } = true;
        public T Data { get; set; }
        public string Msg { get; set; }
    }
}
