using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Prodesp.Core.Backend.Domain.Validations;
using Prodesp.Infra.EF.Helpers;
using System.Data;
using Oracle.EntityFrameworkCore;

using static Prodesp.Infra.EF.UnitOfWorkCore.IUnitOfWork;

namespace Prodesp.Infra.EF.UnitOfWorkCore
{
    public class BaseUnitOfWork<T> : IUnitOfWork<T>, IDisposable where T : DbContext
    {
        private IDbContextTransaction? _transaction;
        public T? _ctx = default(T);

        public BancoHelper.TipoBanco Banco;

        protected virtual string ConnectionStringName { get; set; }
        //public virtual T Contexto { get; set; }
        public BaseUnitOfWork(string connectionStringName, BancoHelper.TipoBanco tipoBanco)
        {

            this.ConnectionStringName = ConnectionStringToken.Parse(connectionStringName).ConnectionString;

            //Console.WriteLine()
            /*var conToken = new ConnectionStringToken();
            conToken.ConnectionString = this.ConnectionStringName;
            conToken.Saltkey = "M1Z0N2X9";
            Console.WriteLine(conToken.ToString());*/
            this.Banco = tipoBanco;
        }

        public virtual T Contexto
        {
            get
            {
                var optionsBuilder = new DbContextOptionsBuilder<T>();
                if (this.Banco == BancoHelper.TipoBanco.SQLServer)
                {
                    optionsBuilder.UseSqlServer(this.ConnectionStringName);
                }
                else
                {
                    optionsBuilder.UseOracle(this.ConnectionStringName);
                }

                var _loggerFactory
                    = LoggerFactory.Create(builder => builder.AddDebug().AddFilter((category, level) => level == LogLevel.Information && !category.EndsWith("Connection")));


                optionsBuilder.UseLoggerFactory(_loggerFactory);

                if (this._ctx == null)
#pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
                    this._ctx = (T)Activator.CreateInstance(typeof(T), (object)optionsBuilder.Options);
#pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
#pragma warning disable CS8603 // Possível retorno de referência nula.
                return this._ctx;
#pragma warning restore CS8603 // Possível retorno de referência nula.
            }
        }

        public virtual void Dispose()
        {
            ((DbContext)(object)this.Contexto).Dispose();
            if (this._transaction == null)
                return;
            this._transaction.Dispose();
            this._transaction = null;
        }

        public virtual void Rollback()
        {
            if (this._transaction == null)
                return;
            this._transaction.Rollback();
            this._transaction = null;
        }

        public virtual void Commit()
        {
            if (this._transaction == null)
                return;
            this._transaction.Commit();
            this._transaction = null;
        }

        public virtual IDbContextTransaction? BeginTransaction(
          IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted)
        {
            if (this._transaction == null)
                this._transaction = ((DbContext)(object)this.Contexto)?.Database?.BeginTransaction(isolationLevel);
            return this._transaction;
        }

        public virtual ValidationResult Save()
        {
            ValidationResult validationResult = new ValidationResult();
            try
            {
                ((DbContext)(object)this.Contexto).SaveChanges();
            }
            /* catch (DbEntityValidationException ex) - não funciona na nova versão do EntityFramework
             {
                 string errorMessage = string.Join(Environment.NewLine, ex.EntityValidationErrors.SelectMany<DbEntityValidationResult, DbValidationError>((Func<DbEntityValidationResult, IEnumerable<DbValidationError>>)(x => (IEnumerable<DbValidationError>)x.ValidationErrors)).Select<DbValidationError, string>((Func<DbValidationError, string>)(x => x.ErrorMessage)));
                 validationResult.Add(errorMessage);
             }*/
            catch (Exception ex)
            {
                string innerException = this.GetInnerException(ex);
                validationResult.Add(innerException);
            }
            return validationResult;
        }

        public async virtual Task<ValidationResult> SaveAsync()
        {
            ValidationResult validationResult = new ValidationResult();
            try
            {
                await ((DbContext)(object)this.Contexto).SaveChangesAsync();
            }
            /* catch (DbEntityValidationException ex) - não funciona na nova versão do EntityFramework
             {
                 string errorMessage = string.Join(Environment.NewLine, ex.EntityValidationErrors.SelectMany<DbEntityValidationResult, DbValidationError>((Func<DbEntityValidationResult, IEnumerable<DbValidationError>>)(x => (IEnumerable<DbValidationError>)x.ValidationErrors)).Select<DbValidationError, string>((Func<DbValidationError, string>)(x => x.ErrorMessage)));
                 validationResult.Add(errorMessage);
             }*/
            catch (Exception ex)
            {
                string innerException = this.GetInnerException(ex);
                validationResult.Add(innerException);
            }
            return validationResult;
        }

        public string GetInnerException(Exception ex) => ex.InnerException != null ? string.Format("{0} > {1} ", (object)ex.InnerException.Message, (object)this.GetInnerException(ex.InnerException)) : string.Empty;
    }
}
