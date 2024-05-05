using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.RESTAPIDto;
public class Tag
{
    public string en { get; set; }
}

public class Tags
{
    public float confidence { get; set; }
    public Tag tag { get; set; }
}

public class Result
{
    public List<Tags> tags { get; set; }
}
public class TaggerDto
{
    public Result result { get; set; }
}