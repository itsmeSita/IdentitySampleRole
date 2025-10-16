using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    // yeuta generic response banaune jun sab ma use garna milos
    public class ServiceResponse<T>
    {
        public T Data { get; set; } 
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
