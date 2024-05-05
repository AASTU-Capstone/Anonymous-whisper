using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.RESTAPIDto.Common;

public class AI
{
    public bool is_detected { get; set; }
}
public class Report
{
    public string verdict { get; set; }
    public AI ai { get; set; }
    public AI human { get; set; }

}
