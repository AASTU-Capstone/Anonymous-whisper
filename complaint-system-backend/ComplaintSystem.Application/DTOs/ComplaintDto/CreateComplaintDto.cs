using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ComplaintSystem.Application.DTOs.ComplaintDto
{
    public class CreateComplaintDto
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public IFormFile? ImageEvidence { get; set; }
        public IFormFile? SoundTrack { get; set; }
        public string? Category { get; set; }
        public string? Tag { get; set; }
        public string? Status { get; set; }
        public Guid UserEntityId { get; set; }
    }
}