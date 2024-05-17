using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Infrastructure.services.Common;
public class Request
{
    public string id { get; set; }
    public double timestamp { get; set; }
    public int operations { get; set; }
}
public class Type
{
    public double ai_generated { get; set; }
}
public class Media
{
    public string id { get; set; }
    public string uri { get; set; }
}
public class SightEngineResponse
{
    public string status {  get; set; }
    public Request request { get; set; }
    public Type type { get; set; }
    public Media media { get; set; }
}
