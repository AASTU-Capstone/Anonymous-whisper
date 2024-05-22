using ComplaintSystem.Application.DTOs.PaginationDto;
using ComplaintSystem.Domain.Entities;

namespace ComplaintSystem.Application.Persistence.Contracts
{
    public interface ISubordinateRepository : IGenericRepository<Subordinate>
    {

        #region Entity Specific Methods

        public Task<List<Subordinate>> GetSubordinatesForManager(Guid ManagerId, PaginationDto paginationDto);
        public Task<List<Subordinate>> SearchSubordinates(string Keyword, PaginationDto paginationDto);
        public Task<Subordinate> GetSubordinateByUserId(Guid UserId);

        #endregion


        #region Count Methods

        public Task<int> GetSubordinatesForManagerCount(Guid ManagerId);
        public Task<int> SearchSubordinatesCount(string Keyword);

        #endregion
    }
}