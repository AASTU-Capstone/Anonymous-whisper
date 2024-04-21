using ComplaintSystem.Domain.Entities.Common;

namespace ComplaintSystem.Domain.Entities
{
    public class Subordinate : BaseEntity
    {
        public string? Name { get; set; }
        public int MitigatedCount { get; set; }
        public Guid ManagerId { get; set; }
    }
}