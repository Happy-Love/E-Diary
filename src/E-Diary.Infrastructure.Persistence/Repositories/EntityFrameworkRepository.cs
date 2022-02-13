using E_Diary.Domain;
using E_Diary.Domain.Entities.Interfaces;
using E_Diary.Infrastructure.Persistence.Extensions.Specification;
using Microsoft.EntityFrameworkCore;

namespace Monolith.Diploma.Persistence.Repositories;

public class EntityFrameworkRepository<T> : IRepository<T> where T : class
{
    private readonly DbContext dbContext;

    private DbSet<T> EntitiesSet => dbContext.Set<T>();

    public EntityFrameworkRepository(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Add(T entity)
    {
        ArgumentNullException.ThrowIfNull(nameof(entity));

        await EntitiesSet.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddRange(IEnumerable<T> entities)
    {
        ArgumentNullException.ThrowIfNull(nameof(entities));

        await EntitiesSet.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        ArgumentNullException.ThrowIfNull(nameof(entity));

        EntitiesSet.Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateRange(IEnumerable<T> entities)
    {
        ArgumentNullException.ThrowIfNull(nameof(entities));

        EntitiesSet.UpdateRange(entities);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        ArgumentNullException.ThrowIfNull(nameof(entity));

        EntitiesSet.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async ValueTask<T?> GetById(object id)
    {
        ArgumentNullException.ThrowIfNull(nameof(id));

        var result = await EntitiesSet.FindAsync(id);
        return result;
    }

    public async Task<T[]> List(ISpecification<T> specification, int skip = 0, int take = 100)
    {
        var result = await EntitiesSet
            .Specify(specification)
            .Skip(skip)
            .Take(take)
            .ToArrayAsync();

        return result;
    }
}