using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.ComplaintLogDto;

public class AssignSubordinateControllerDto
{
    public Guid ComplaintLogId { get; set; }
    public Guid SubordinateId { get; set; }
}
