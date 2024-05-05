using ComplaintSystem.Domain.Entities.Common;

namespace ComplaintSystem.Domain.Entities
{
    public class Subordinate : BaseEntity
    {
        public string Name { get; set; }
        public string Email {  get; set; }
        public int MitigatedCount { get; set; } = 0;
        public Guid ManagerId { get; set; }
    }
}