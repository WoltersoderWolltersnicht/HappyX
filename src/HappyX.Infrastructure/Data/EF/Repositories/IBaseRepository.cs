using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace HappyX.Infrastructure.Data.EF.Repositories;

public interface IBaseRepository<TEntity>
{
    public void InitDbContext(DbContext? context);
    public Task<IEnumerable<TEntity>> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "");
    public Task<TEntity?> GetById(int id);
    public TEntity Insert(TEntity entity);
    public void InsertMany(IEnumerable<TEntity> entities);
    public void Delete(int id);
    public void Delete(TEntity entityToDelete);
    public void Update(TEntity entityToUpdate);
}