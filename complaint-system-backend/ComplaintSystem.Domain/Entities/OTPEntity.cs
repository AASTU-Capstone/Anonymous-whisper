using  ComplaintSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  ComplaintSystem.Domain.Entities;
public class OTPEntity : BaseEntity
{
    public String Otp {  get; set; }
    public Guid EntityId { get; set; }
}
