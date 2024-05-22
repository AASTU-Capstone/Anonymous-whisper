using ComplaintSystem.Domain.Entities;
using ComplaintSystem.Application.DTOs.PaginationDto;

namespace ComplaintSystem.Application.Persistence.Contracts
{
    public interface IComplaintRepository : IGenericRepository<Complaint>
    {

        #region Entity Specific Methods

        public Task<List<Complaint>> GetUserComplaints(Guid UserId, PaginationDto paginationDto);
        public Task<List<Complaint>> GetUserComplaintsByStatus(Guid UserId, string Status, PaginationDto paginationDto);
        public Task<List<Complaint>> GetAcceptedComplaints(PaginationDto paginationDto);
        public Task<List<Complaint>> GetMatchingComplaints(string Keyword, PaginationDto paginationDto);

        #endregion


        #region Count Methods
        public Task<int> GetUserAcceptedComplaintsCount(Guid UserId);
        public Task<int> GetUserRejectedComplaintsCount(Guid UserId);
        public Task<int> GetRecievedComplaintsCount();
        public Task<int> GetMatchingComplaintsCount(string Keyword);

        #endregion
        public Task<List<Complaint>> GetComplaintsForAdminByStatus(string status);
        public Task<List<Complaint>> GetUserComplaints(Guid UserId);
        public Task<List<Complaint>> GetUserComplaintsByStatus(Guid UserId, string Status);
        public Task<List<Complaint>> GetMatchingComplaints(string Keyword, string category, string dateOrder);
    }
}