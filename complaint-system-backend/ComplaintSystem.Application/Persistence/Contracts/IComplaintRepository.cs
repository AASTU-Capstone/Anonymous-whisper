using ComplaintSystem.Domain.Entities;
using ComplaintSystem.Application.DTOs.PaginationDto;
using ComplaintSystem.Application.DTOs.ComplaintDto;

namespace ComplaintSystem.Application.Persistence.Contracts
{
    public interface IComplaintRepository : IGenericRepository<Complaint>
    {

        #region Entity Specific Methods

        public Task<List<Complaint>> GetUserComplaints(Guid UserId, PaginationDto paginationDto);
        public Task<List<Complaint>> GetUserComplaintsByStatus(Guid UserId, string Status, PaginationDto paginationDto);
        public Task<List<Complaint>> GetComplaintsForAdminByStatus(string status, PaginationDto paginationDto);
        public Task<List<Complaint>> GetMatchingComplaints(string Keyword, string category, string dateOrder, PaginationDto paginationDto);
        public Task<GetComplaintStatisticsDto> GetComplaintStatistics(Guid? UserId);
        public Task<List<Complaint>> GetAllComplaintsForAdmin(PaginationDto paginationDto);
        public Task<List<Complaint>> GetAllComplaintsForUser(Guid UserId, PaginationDto paginationDto);

        #endregion


        #region Count Methods
        public Task<int> GetUserAcceptedComplaintsCount(Guid UserId);
        public Task<int> GetUserRejectedComplaintsCount(Guid UserId);
        public Task<int> GetAllComplaintsForAdminCount();
        public Task<int> GetAllUserComplaintsCount(Guid UserId);
        public Task<int> GetComplaintsForAdminByStatusCount(string status);
        public Task<int> GetMatchingComplaintsCount(string Keyword, string category);

        #endregion
       
    }
}