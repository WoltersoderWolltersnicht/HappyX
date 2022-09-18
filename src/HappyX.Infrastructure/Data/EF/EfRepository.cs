using System.Linq.Expressions;
using HappyX.Domain.Internal;
using HappyX.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HappyX.Infrastructure.Data.EF;

public sealed class EfRepository<TEntity> : IBaseRepository<TEntity>
 where TEntity : BaseEntity
{
    private DbContext _context;
    private DbSet<TEntity> _dbSet;
    
    public void InitDbContext(DbContext context)
    {
        _context = context ?? throw  new Exception("Invalid Database Context");
        _dbSet = context?.Set<TEntity>() ?? throw new Exception($"DbSet {typeof(TEntity)} not found");
    }

    public async Task<IEnumerable<TEntity>> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet;

        foreach (var includeProperty in includeProperties.Split
                     (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }
        
        if (filter != null)
        {
            query = query.Where(filter);
        }



        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }
    
    public async Task<TEntity> GetById(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public TEntity Insert(TEntity entity)
    {
        return _dbSet.Add(entity).Entity;
    }

    public void InsertMany(IEnumerable<TEntity> entities)
    {
        _dbSet.AddRange(entities);
    }

    public async void Delete(int id)
    {
        var entityToDelete = await GetById(id) ?? throw new InvalidOperationException();
        Delete(entityToDelete);
    }

    public void Delete(TEntity entityToDelete)
    {
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
        {
            _dbSet.Attach(entityToDelete);
        }
        _dbSet.Remove(entityToDelete);
    }

    public void Update(TEntity entityToUpdate)
    {
        _dbSet.Attach(entityToUpdate);
        _context.Entry(entityToUpdate).State = EntityState.Modified;
    }
}