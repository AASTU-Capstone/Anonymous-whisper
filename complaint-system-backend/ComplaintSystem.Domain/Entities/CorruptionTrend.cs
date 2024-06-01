using ComplaintSystem.Domain.Entities.Common;

namespace ComplaintSystem.Domain.Entities
{
    public class CorruptionTrend : BaseEntity
    {
        public string? Name { get; set; }
        public int MitigatedCount { get; set; }
        public int TotalCount { get; set; }
    }
}