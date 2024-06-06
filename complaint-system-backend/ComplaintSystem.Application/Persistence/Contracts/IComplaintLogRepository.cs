using ComplaintSystem.Application.DTOs.ComplaintLogDto;
using ComplaintSystem.Application.DTOs.PaginationDto;
using ComplaintSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Persistence.Contracts;
public interface IComplaintLogRepository : IGenericRepository<ComplaintLog>
{

    #region Entity Specific Methods

    public Task<List<ComplaintLog>> GetForManager(Guid ManagerId, string Status, PaginationDto paginationDto);
    public Task<List<ComplaintLog>> GetForAdmin(Guid AdminId, string Status, PaginationDto paginationDto);
    public Task<List<ComplaintLog>> GetForSubordinate(Guid SubordinateId, string Status, PaginationDto paginationDto);
    public Task<List<ComplaintLog>> GetByStatus(string Status, PaginationDto paginationDto);
    public Task<GetComplaintLogStatisticsDto> GetComplaintLogStatistics(Guid? ManagerId, Guid? SubordinateId);
    public Task<List<ComplaintLog>> GetComplaintLogsBySubordinateId(Guid SubordinateId);

    #endregion


    #region Count Methods

    public Task<int> GetForManagerCount(Guid ManagerId, string Status);
    public Task<int> GetForAdminCount(Guid AdminId, string Status);
    public Task<int> GetForSubordinateCount(Guid SubordinateId, string Status);
    public Task<int> GetByStatusCount(string Status);

    #endregion
}

