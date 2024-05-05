using ComplaintSystem.Domain.Entities.Common;

namespace ComplaintSystem.Domain.Entities
{
    public class Complaint: BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string>? ImageEvidences { get; set; }
        public List<string>? SoundTracks { get; set; }
        public List<string>? Documents { get; set; }
        public string Category { get; set; }
        public List<string>? Tag { get; set; }
        public string Status { get; set; }
        public Guid UserEntityId { get; set; }
    }
}