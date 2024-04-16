using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.Persistence;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetPaginatedAsync(int pageNumber, int pageSize);
    Task<T> GetAsync(Guid id);
    Task<bool> EntityExists(Guid id);
    Task<T> Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}
