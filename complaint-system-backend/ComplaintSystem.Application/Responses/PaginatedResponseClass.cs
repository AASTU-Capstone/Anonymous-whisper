using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  ComplaintSystem.Application.Responses
{
    public class PaginatedResponseClass
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public object Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<string> Error { get; set; }
    }
}
