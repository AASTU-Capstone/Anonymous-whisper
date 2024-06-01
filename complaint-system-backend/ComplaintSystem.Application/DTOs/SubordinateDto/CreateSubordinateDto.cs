using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.SubordinateDto
{
    public class CreateSubordinateDto
    {
        public string Name { get; set; }
        public string Email {  get; set; }
        public int MitigatedCount { get; set; }
        public Guid UserEntityId { get; set; }
        public Guid ManagerId { get; set; }
    }
}