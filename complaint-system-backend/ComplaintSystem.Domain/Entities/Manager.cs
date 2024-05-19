using ComplaintSystem.Domain.Entities.Common;

namespace ComplaintSystem.Domain.Entities
{
    public class Manager: BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public Guid UserEntityId { get; set; }
        public Guid AdminId { get; set; }        
    }
}