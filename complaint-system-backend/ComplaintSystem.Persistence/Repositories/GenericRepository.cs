using ComplaintSystem.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly CompliantSystemAppDbContext _context;
    public GenericRepository(CompliantSystemAppDbContext sparkTankAppDbContext)
    {
        _context = sparkTankAppDbContext;
    }
    public async Task<T> Add(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;

    }

    public async Task Delete(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }


    public async Task<bool> EntityExists(Guid id)
    {
        var entity = await GetAsync(id);
        return entity != null;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var entities = await _context.Set<T>().ToListAsync();
        return entities;
    }

    public async Task<T> GetAsync(Guid id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        return entity;
    }

    public async Task<IEnumerable<T>> GetPaginatedAsync(int pageNumber, int pageSize)
    {
        var items = await _context.Set<T>().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return items;
    }

    public async Task Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
