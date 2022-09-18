using System.Linq.Expressions;
using AutoFixture;
using HappyX.Domain.Internal;
using HappyX.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HappyX.Infrastructure.Data.Mock;

public class MockRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    private Fixture _fixture;

    public MockRepository()
    {
        var fixture = new Fixture();
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior(3));
        _fixture = fixture;
    }

    public void InitDbContext(DbContext context)
    {
    }

    public Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
    {
       return Task.FromResult(_fixture.CreateMany<TEntity>());
    }

    public Task<TEntity> GetById(int id)
    {
        return Task.FromResult(_fixture.Create<TEntity>());
    }

    public TEntity Insert(TEntity entity)
    {
        return _fixture.Create<TEntity>();
    }

    public void InsertMany(IEnumerable<TEntity> entities)
    {
    }

    public void Delete(int id)
    {
    }

    public void Delete(TEntity entityToDelete)
    {
    }

    public void Update(TEntity entityToUpdate)
    {
    }
}