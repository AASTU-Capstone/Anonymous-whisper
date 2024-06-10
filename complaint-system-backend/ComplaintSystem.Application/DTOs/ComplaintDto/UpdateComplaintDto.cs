using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.ComplaintDto;
public class UpdateComplaintDto
{
    public Guid ComplaintId { get; set; }
    public string Status { get; set; }
    public string? Feedback { get; set; }
}
