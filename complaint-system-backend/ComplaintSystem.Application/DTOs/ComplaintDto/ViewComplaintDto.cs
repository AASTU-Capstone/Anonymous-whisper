using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.ComplaintDto;
public class ViewComplaintDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public List<string>? Tag { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
