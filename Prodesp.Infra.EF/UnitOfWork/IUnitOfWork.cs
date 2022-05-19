
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Prodesp.Core.Backend.Domain.Validations;
using System.Data;


namespace Prodesp.Infra.EF.UnitOfWorkCore;

public interface IUnitOfWork : IDisposable 
{
    public interface IUnitOfWork<T> : IDisposable where T : DbContext
    {
        IDbContextTransaction? BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted);

        ValidationResult Save();

        Task<ValidationResult> SaveAsync();

        void Rollback();

        void Commit();

        T Contexto { get; }
    }
}