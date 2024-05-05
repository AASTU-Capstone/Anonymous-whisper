using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.ComplaintLogDto;
public class UpdateComplaintLogDto
{
    public Guid Id { get; set; }
    public string? Report { get; set; }
}
