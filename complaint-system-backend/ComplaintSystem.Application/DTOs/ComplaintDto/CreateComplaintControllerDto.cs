using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ComplaintSystem.Application.DTOs.ComplaintDto
{
    public class CreateComplaintControllerDto
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public List<IFormFile>? ImageEvidence { get; set; }
        public List<IFormFile>? SoundTrack { get; set; }
        public List<IFormFile>? Documents { get; set; }
        public string? Category { get; set; }
    }
}