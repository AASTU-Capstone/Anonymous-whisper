using ComplaintSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Domain.Entities;
public class ListingsEntity : BaseEntity
{
    public string Title {  get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
}
