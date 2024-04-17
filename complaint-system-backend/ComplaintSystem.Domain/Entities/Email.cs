using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  ComplaintSystem.Domain.Entities;

public class Email
{
    public string To { get; set; } = null!;
    public string? Subject { get; set; }
    public string? Body { get; set; }
}
