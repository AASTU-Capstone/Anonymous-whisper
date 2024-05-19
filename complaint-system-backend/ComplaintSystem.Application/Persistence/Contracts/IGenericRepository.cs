using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplaintSystem.Application.DTOs.PaginationDto;

namespace ComplaintSystem.Application.Persistence.Contracts;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<int> CountAsync();
    Task<T> GetAsync(Guid id);
    Task<bool> EntityExists(Guid id);
    Task<T> Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}
