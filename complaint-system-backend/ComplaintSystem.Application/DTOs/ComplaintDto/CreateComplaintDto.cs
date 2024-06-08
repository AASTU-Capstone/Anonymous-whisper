using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.ComplaintDto;

public class CreateComplaintDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public List<string>? ImageEvidences { get; set; }
    public List<string>? SoundTracks { get; set; }
    public List<string>? Documents { get; set; }
    public List<string>? Videos { get; set; }
    public string Category { get; set; }
    public List<string>? Tag { get; set; }
    public string Status { get; set; }
    public Guid UserEntityId { get; set; }
}
