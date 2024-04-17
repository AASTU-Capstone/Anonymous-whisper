using  ComplaintSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  ComplaintSystem.Domain.Entities;

public class UserEntity:BaseEntity
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? User_Type { get; set; }
}
