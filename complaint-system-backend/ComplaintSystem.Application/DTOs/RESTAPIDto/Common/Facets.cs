using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.RESTAPIDto.Common;

public class Quality
{
    public string version { get; set; }
    public bool is_detected {  get; set; }  
}
public class Facets
{

    public Quality quality { get; set; }
    public Quality nsfw { get; set; }
}
