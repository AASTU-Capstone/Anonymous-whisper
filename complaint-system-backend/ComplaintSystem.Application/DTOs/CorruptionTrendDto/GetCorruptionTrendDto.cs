using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.CorruptionTrendDto;
public class GetCorruptionTrendDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int MitigatedCount { get; set; }
    public int TotalCount { get; set; }
}
