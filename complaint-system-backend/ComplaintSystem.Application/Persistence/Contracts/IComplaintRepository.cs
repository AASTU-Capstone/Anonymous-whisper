using ComplaintSystem.Domain.Entities;

namespace ComplaintSystem.Application.Persistence.Contracts
{
    public interface IComplaintRepository : IGenericRepository<Complaint>
    {
        public Task<List<Complaint>> GetAcceptedComplaints();
        public Task<List<Complaint>> GetUserComplaints(Guid UserId);
        public Task<List<Complaint>> GetUserComplaintsByStatus(Guid UserId, string Status);
        public Task<List<Complaint>> GetMatchingComplaints(string Keyword);
    }
}