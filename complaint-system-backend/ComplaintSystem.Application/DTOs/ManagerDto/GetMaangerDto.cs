using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.ManagerDto;

public class GetMaangerDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Role { get; set; }
}
