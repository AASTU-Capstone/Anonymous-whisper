using ComplaintSystem.Application.DTOs.PaginationDto;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Persistence.Repositories;
public class ResourceRepository : GenericRepository<Resource>, IResourceRepository
{
    private readonly ComplaintSystemAppDbContext _context;
    public ResourceRepository(ComplaintSystemAppDbContext complaintSystemAppDbContext) : base(complaintSystemAppDbContext)
    {
        _context = complaintSystemAppDbContext;
    }

    public async Task<List<Resource>> GetAllResources(PaginationDto paginationDto)
    {
        var resources = await _context.Resources.Skip((criteria.PageNumber - 1) * criteria.PageSize)
                .Take(criteria.PageSize)
                .ToListAsync();

        return resources;
    }

    public async Task<int> GetResourcesCount()
    {
        var resourceCount = await _context.Resources.CountAsync();
        return resourceCount;
    }
}
