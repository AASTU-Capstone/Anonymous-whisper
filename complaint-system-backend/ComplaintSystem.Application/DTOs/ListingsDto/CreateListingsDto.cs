using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.ListingsDto;
public class CreateListingsDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
}
