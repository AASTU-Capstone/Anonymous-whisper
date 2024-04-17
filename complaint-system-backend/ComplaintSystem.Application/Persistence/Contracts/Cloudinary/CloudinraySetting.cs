using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  ComplaintSystem.Application.Persistence.Contracts.Cloudinary
{
    public class CloudinarySetting
    {
        public const string SectionName = "Cloudinary settings";
        public string? CloudName { get; set; }
        public string? ApiKey { get; set; }
        public string? ApiSecret { get; set; }
    }
}
