
using Prodesp.Core.Backend.Domain.Interfaces;
using Prodesp.Core.Backend.Domain.Interfaces.Domain.Services;
using Prodesp.Core.Backend.Domain.Models;
using Prodesp.Domain.Shared.Entities;

namespace Prodesp.Domain.Services.Interfaces;

public interface ILoginInteraction : IEntityInteractions<Login>
{
    Task<Login?> GetLoginByToken(string token);
    

    Task<Login?> GetLoginAsync(string idLogin);
    Task<PageResult<Login>?> GetAll();
    Task<Login?> Login(string login);

    Task<Sessao?> GerarSessao(string ip, Login login, string userAgent, int TempoExpiracao, string TokenJWT);
    Task<bool> ZerarTentativasLogin(Login login);
    Task<bool> AtualizarLogin(Login login);
    Task<Login?> AddQuantidadeTentativaLogin(Login login);

}

public interface ILoginService : IService<Shared.Entities.Login>, ILoginInteraction
{
}
