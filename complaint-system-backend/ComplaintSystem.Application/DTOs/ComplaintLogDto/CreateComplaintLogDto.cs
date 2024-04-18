using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.ComplaintLogDto
{
    public class CreateComplaintLogDto
    {
        public string? Title { get; set; }
        public string? Report { get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }
        public Guid ComplaintId { get; set; }
    }
}