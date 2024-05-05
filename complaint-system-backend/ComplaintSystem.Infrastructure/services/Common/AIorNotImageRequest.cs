using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Infrastructure.services.Common;

public class AIorNotImageRequest
{
    public IFormFile image {  get; set; }
}
