
using Prodesp.Core.Backend.Domain.Interfaces;
using Prodesp.Core.Backend.Domain.Interfaces.Domain.Services;
using Prodesp.Core.Backend.Domain.Models;

namespace Prodesp.Domain.Services.Interfaces;

    public interface ISessaoInteraction : IEntityInteractions<Shared.Entities.Sessao>
    {
        Task<Shared.Entities.Sessao?> GetSessao(string idSessao);
        Task<PageResult<Shared.Entities.Sessao>?> GetAll();
        Task<Shared.Entities.Sessao?> FindByAccessToken(string cdToken);
    }


public interface ISessaoService : IService<Shared.Entities.Sessao>, ISessaoInteraction
{
}