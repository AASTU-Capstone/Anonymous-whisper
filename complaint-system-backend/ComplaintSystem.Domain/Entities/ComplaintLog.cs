using ComplaintSystem.Domain.Entities.Common;

namespace ComplaintSystem.Domain.Entities
{
    public class ComplaintLog: BaseEntity
    {
        public string? Title { get; set; }
        public string? Report { get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }
        public Guid ComplaintId { get; set; }        
    }
}