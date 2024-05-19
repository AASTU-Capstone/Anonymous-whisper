using ComplaintSystem.Application.DTOs.PaginationDto;
using ComplaintSystem.Application.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ComplaintSystem.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ComplaintSystemAppDbContext _context;
    public GenericRepository(ComplaintSystemAppDbContext complaintSystemAppDbContext)
    {
        _context = complaintSystemAppDbContext;
    }

    // Add entity to the database
    public async Task<T> Add(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;

    }


    // Count the number of entities in the database
    public async Task<int> CountAsync()
    {
        return await _context.Set<T>().CountAsync();
    }


    // Delete entity from the database
    public async Task Delete(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }


    // Check if entity exists in the database
    public async Task<bool> EntityExists(Guid id)
    {
        var entity = await GetAsync(id);
        return entity != null;
    }


    // Get all entities from the database
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var entities = await _context.Set<T>()
            .OrderByDescending(x => EF.Property<DateTime>(x, "CreatedAt"))
            .ToListAsync();
        return entities;
    }


    // Get entity by id from the database
    public async Task<T> GetAsync(Guid id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        return entity;
    }


    // Update entity in the database
    public async Task Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
