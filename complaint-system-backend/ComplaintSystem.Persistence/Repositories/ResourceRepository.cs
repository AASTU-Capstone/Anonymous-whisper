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

    public Task<List<Resource>> GetAllResources(PaginationDto paginationDto)
    {
        throw new NotImplementedException();
    }
}
