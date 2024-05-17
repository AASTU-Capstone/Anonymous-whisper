using ComplaintSystem.Domain.Entities;
using ComplaintSystem.Application.DTOs.PaginationDto;

namespace ComplaintSystem.Application.Persistence.Contracts
{
    public interface IComplaintRepository : IGenericRepository<Complaint>
    {
        public Task<List<Complaint>> GetAcceptedComplaints();
        public Task<List<Complaint>> GetUserComplaints(Guid UserId, PaginationDto paginationDto);
        public Task<List<Complaint>> GetUserComplaintsByStatus(Guid UserId, string Status, PaginationDto paginationDto);
        public Task<List<Complaint>> GetMatchingComplaints(string Keyword);
    }
}