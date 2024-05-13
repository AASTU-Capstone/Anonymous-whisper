using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.ComplaintLogDto;

public class UpdateComplaintLogStatusDto
{
    public Guid ComplainLogId { get; set; }
    public Guid StatusChangerId {  get; set; }
    public string Status { get; set; }
}
