using ComplaintSystem.Application.DTOs.ComplaintLogDto;
using ComplaintSystem.Application.DTOs.PaginationDto;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Persistence.Repositories
{
    public class ComplaintLogRepository : GenericRepository<ComplaintLog>, IComplaintLogRepository
    {
        public readonly ComplaintSystemAppDbContext _complaintSystemAppDbContext;
        public ComplaintLogRepository(ComplaintSystemAppDbContext complaintSystemAppDbContext) : base(complaintSystemAppDbContext)
        {
            _complaintSystemAppDbContext = complaintSystemAppDbContext;
        }


        #region Entity Specific Methods Implementation

        // Get Complaint Logs for Manager
        public async Task<List<ComplaintLog>> GetForManager(Guid ManagerId, string Status, PaginationDto paginationDto)
        {
            var complaints = await _complaintSystemAppDbContext.ComplaintLogs
                .Where(log => log.ManagerId == ManagerId && log.Status.ToLower() == Status)
                .Skip((paginationDto.PageNumber - 1) * paginationDto.PageSize)
                .Take(paginationDto.PageSize)
                .ToListAsync();

            return complaints;
        }

        // Get Complaint Logs for Admin
        public async Task<List<ComplaintLog>> GetForAdmin(Guid AdminId, string Status, PaginationDto paginationDto)
        {
            var complaints = await _complaintSystemAppDbContext.ComplaintLogs
                .Where(log => log.AdminId == AdminId && log.Status.ToLower() == Status)
                .Skip((paginationDto.PageNumber - 1) * paginationDto.PageSize)
                .Take(paginationDto.PageSize)
                .ToListAsync();

            return complaints;
        }

        // Get Complaint Logs for Subordinate
        public async Task<List<ComplaintLog>> GetForSubordinate(Guid SubordinateId, string Status, PaginationDto paginationDto)
        {
            var complaints = await _complaintSystemAppDbContext.ComplaintLogs
                .Where(log => log.SubordinateId == SubordinateId && log.Status.ToLower() == Status)
                .Skip((paginationDto.PageNumber - 1) * paginationDto.PageSize)
                .Take(paginationDto.PageSize)
                .ToListAsync();

            return complaints;
        }

        // Get Complaint Logs by Status
        public async Task<List<ComplaintLog>> GetByStatus(string Status, PaginationDto paginationDto)
        {
            var complaints = await _complaintSystemAppDbContext.ComplaintLogs
                .Where(log => log.Status == Status)
                .Skip((paginationDto.PageNumber - 1) * paginationDto.PageSize)
                .Take(paginationDto.PageSize)
                .ToListAsync();

            return complaints;
        }

        #endregion


        #region Count Methods Implementation

        // Get Count of Complaint Logs for Manager
        public async Task<int> GetForManagerCount(Guid ManagerId, string Status)
        {
            var count = await _complaintSystemAppDbContext.ComplaintLogs.Where(log => log.ManagerId == ManagerId && log.Status.ToLower() == Status).CountAsync();
            return count;
        }

        // Get Count of Complaint Logs for Admin
        public async Task<int> GetForAdminCount(Guid AdminId, string Status)
        {
            var count = await _complaintSystemAppDbContext.ComplaintLogs.Where(log => log.AdminId == AdminId && log.Status.ToLower() == Status).CountAsync();
            return count;
        }

        // Get Count of Complaint Logs for Subordinate
        public async Task<int> GetForSubordinateCount(Guid SubordinateId, string Status)
        {
            var count = await _complaintSystemAppDbContext.ComplaintLogs.Where(log => log.SubordinateId == SubordinateId && log.Status.ToLower() == Status).CountAsync();
            return count;
        }

        // Get Count of Complaint Logs by Status
        public async Task<int> GetByStatusCount(string Status)
        {
            var count = await _complaintSystemAppDbContext.ComplaintLogs.Where(log => log.Status == Status).CountAsync();
            return count;
        }

        public async Task<GetComplaintLogStatisticsDto> GetComplaintLogStatistics(Guid? ManagerId, Guid? SubordinateId)
        {
            int totalComplaintLogCount = 0;
            int PendingComplaintLogCount = 0;
            int ResolvedComplaintLogCount = 0;
            int AssignedComplaintLogCount = 0;
            if(ManagerId != null)
            {
                var query = _complaintSystemAppDbContext.ComplaintLogs.Where(complog => complog.ManagerId == ManagerId);
                totalComplaintLogCount = await query.CountAsync();
                PendingComplaintLogCount = await query.Where(comp => comp.Status.ToLower() == "overviewing" || comp.Status.ToLower() == "progressing"|| comp.Status.ToLower() == "submitted").CountAsync();
                ResolvedComplaintLogCount = await query.Where(comp => comp.Status.ToLower() == "resolved").CountAsync();
                AssignedComplaintLogCount = await query.Where(comp => comp.Status.ToLower() == "processing").CountAsync();

            }
            else if(SubordinateId != null)
            {
                var query = _complaintSystemAppDbContext.ComplaintLogs.Where(complog => complog.SubordinateId == SubordinateId);
                totalComplaintLogCount = await query.CountAsync();
                PendingComplaintLogCount = await query.Where(comp => comp.Status.ToLower() == "processing").CountAsync();
                ResolvedComplaintLogCount = await query.Where(comp => comp.Status.ToLower() == "resolved").CountAsync();
            }

            GetComplaintLogStatisticsDto getComplaintLogStatisticsDto = new GetComplaintLogStatisticsDto
            {
                PendingComplaintLogs = PendingComplaintLogCount,
                ResolvedComplaintLogs = ResolvedComplaintLogCount,
                TotalComplaintLogs = totalComplaintLogCount,
                AssignedComplaintLogs = AssignedComplaintLogCount,
            };

            return getComplaintLogStatisticsDto;
        }

        #endregion
    }
}
