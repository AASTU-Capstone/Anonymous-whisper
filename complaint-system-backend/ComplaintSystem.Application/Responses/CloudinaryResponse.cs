using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Responses;
public class CloudinaryResponse
{
    public string PublicId { get; set; }
    public string Link {  get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
}
