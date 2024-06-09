using ComplaintSystem.Application.DTOs.PaginationDto;
using ComplaintSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Persistence.Contracts;
public interface IResourceRepository : IGenericRepository<Resource>
{
    public Task<List<Resource>> GetAllResources(PaginationDto paginationDto);
    public Task<int> GetResourcesCount();
}
