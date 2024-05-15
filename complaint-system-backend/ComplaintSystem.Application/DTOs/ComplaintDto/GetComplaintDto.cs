using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.ComplaintDto;
public class GetComplaintDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<string>? ImageEvidences { get; set; }
    public List<string>? Videos { get; set; }
    public List<string>? SoundTracks { get; set; }
    public List<string>? Documents { get; set; }
    public string Category { get; set; }
    public string? Tag { get; set; }
}
