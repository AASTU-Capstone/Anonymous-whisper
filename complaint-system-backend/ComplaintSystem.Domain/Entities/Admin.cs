using ComplaintSystem.Domain.Entities.Common;

namespace ComplaintSystem.Domain.Entities
{
    public class Admin: BaseEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}