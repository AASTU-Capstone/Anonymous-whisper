using ComplaintSystem.Domain.Entities.Common;

namespace ComplaintSystem.Domain.Entities
{
    public class Complaint: BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageEvidence { get; set; }
        public string? SoundTrack { get; set; }
        public string Category { get; set; }
        public string? Tag { get; set; }
        public string Status { get; set; }
        public Guid UserEntityId { get; set; }
    }
}