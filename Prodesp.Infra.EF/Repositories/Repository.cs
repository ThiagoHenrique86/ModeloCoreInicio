using Microsoft.EntityFrameworkCore;
using Prodesp.Core.Backend.Domain.Interfaces;
using Prodesp.Core.Backend.Domain.Interfaces.Infra.Data.Repository;

using System.Linq.Expressions;
using static Prodesp.Infra.EF.UnitOfWorkCore.IUnitOfWork;

namespace Prodesp.Infra.EF;

public class Repository<TEntity, TContext> : IRepository<TEntity>, IEntityInteractions<TEntity>
   where TEntity : class
   where TContext : DbContext
{
    private IUnitOfWork<TContext> _uow;

    protected IUnitOfWork<TContext> UnityOfWork => this._uow;

    public Repository(IUnitOfWork<TContext> uow) => this._uow = uow;

    public virtual void Insert(TEntity entity) => ((DbContext)(object)this._uow.Contexto).Set<TEntity>().Add(entity);

    public virtual void InsertRange(List<TEntity> entity) => ((DbContext)(object)this._uow.Contexto).Set<TEntity>().AddRange((IEnumerable<TEntity>)entity);

    public virtual async Task InsertAsync(TEntity entity) => await Task.Run((Action)(() => this.Insert(entity)));

    public virtual void Delete(TEntity entity) => ((DbContext)(object)this._uow.Contexto).Set<TEntity>().Remove(entity);

    public virtual async Task DeleteAsync(TEntity entity) => await Task.Run((Action)(() => this.Delete(entity)));

    public virtual void Update(TEntity entity) => ((DbContext)(object)this._uow.Contexto).Entry<TEntity>(entity).State = EntityState.Modified;

    public virtual async Task UpdateAsync(TEntity entity) => await Task.Run((Action)(() => this.Update(entity)));

    public virtual async Task<IEnumerable<TEntity>> FindAsync(
      Expression<Func<TEntity, bool>> condition)
    {
        List<TEntity> listAsync = await EntityFrameworkQueryableExtensions.ToListAsync<TEntity>(EntityFrameworkQueryableExtensions.AsNoTracking<TEntity>(((IQueryable<TEntity>)((DbContext)(object)this._uow.Contexto).Set<TEntity>()).Where<TEntity>(condition)));
        return listAsync;
    }

    public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> condition) => (IEnumerable<TEntity>)EntityFrameworkQueryableExtensions.AsNoTracking<TEntity>(((IQueryable<TEntity>)((DbContext)(object)this._uow.Contexto).Set<TEntity>()).Where<TEntity>(condition)).ToList<TEntity>();

    public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> condition) => EntityFrameworkQueryableExtensions.AsNoTracking<TEntity>(((IQueryable<TEntity>)((DbContext)(object)this._uow.Contexto).Set<TEntity>()).Where<TEntity>(condition));

    
    public virtual IEnumerable<TEntity> SelectAll() => (IEnumerable<TEntity>)((IEnumerable<TEntity>)((DbSet<TEntity>)((DbContext)(object)this._uow.Contexto).Set<TEntity>()).AsNoTracking()).ToList<TEntity>();

    public virtual async Task<IEnumerable<TEntity>> SelectAllAsync()
    {
        List<TEntity> listAsync = await EntityFrameworkQueryableExtensions.ToListAsync<TEntity>((IQueryable<TEntity>)((DbSet<TEntity>)((DbContext)(object)this._uow.Contexto).Set<TEntity>()).AsNoTracking());
        return (IEnumerable<TEntity>)listAsync;
    }

#pragma warning disable CS8603 // Possível retorno de referência nula.
    public virtual TEntity SelectById(int key) => ((DbContext)this._uow.Contexto).Set<TEntity>().Find(new object[1]
    {
      (object) key
    });
#pragma warning restore CS8603 // Possível retorno de referência nula.

    public virtual async Task<TEntity> SelectByIdAsync(int key)
    {
#pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
        TEntity async = await ((DbContext)(object)this._uow.Contexto).Set<TEntity>().FindAsync(new object[1]
        {
        (object) key
        });
#pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
#pragma warning disable CS8603 // Possível retorno de referência nula.
        return async;
#pragma warning restore CS8603 // Possível retorno de referência nula.
    }

    public decimal SelectNextVal(string prSequenceName)
    {
        throw new NotImplementedException();
    }
}

