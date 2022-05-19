using Prodesp.Core.Backend.Domain.Interfaces.Infra.Data.Repository;
using Prodesp.Domain.Shared.Entities;

namespace Prodesp.Domain.Repositories.Interfaces
{
    public interface ILoginRepository : IRepository<Login>
    {
        Task<Login?> GetLoginByToken(string token);
        Login? GetLogin(string login);

        public Task<Login?> GetLoginAsync(string login);
    }
}
