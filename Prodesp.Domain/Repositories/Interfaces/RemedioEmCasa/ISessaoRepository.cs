

using Prodesp.Core.Backend.Domain.Interfaces.Infra.Data.Repository;
using Prodesp.Domain.Shared.Entities;

namespace Prodesp.Domain.Repositories.Interfaces
{
    public interface ISessaoRepository : IRepository<Shared.Entities.Sessao>
    {

        Task<Sessao?> GetSessaoByToken(string nome);
    }
}
