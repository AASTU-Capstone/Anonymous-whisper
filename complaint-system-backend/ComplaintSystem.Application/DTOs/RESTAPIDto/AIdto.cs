using ComplaintSystem.Application.DTOs.RESTAPIDto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.RESTAPIDto;

public class AIdto
{
    public Guid id { get; set; }
    public DateTime created_at { get; set; }
    public Report report { get; set; }
    public Facets facets { get; set; }

}
