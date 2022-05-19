
using Prodesp.Core.Backend.Domain.Interfaces;
using Prodesp.Core.Backend.Domain.Interfaces.Domain.Services;
using Prodesp.Core.Backend.Domain.Models;
using Prodesp.Domain.Shared.Entities;

namespace Prodesp.Domain.Services.Interfaces;

public interface IUsuarioInteraction : IEntityInteractions<Shared.Entities.Usuario>
{
    Task<Shared.Entities.Usuario?> GetUsuario(string idUsuario);
    Task<PageResult<Shared.Entities.Usuario>?> GetAll();

    Task<Shared.Entities.Login?> GetUsuarioPorLogin(string cdLogin);

    Task<Core.Backend.Domain.Validations.ValidationResult> AtualizarSenha(string accessToken, string senhaAntiga, string novaSenha);
    Task<Login?> ResetarSenha(Login login, string novaSenha);

   Task<Usuario?> IncluirUsuario(Usuario usuario, Login login);
    Task<Usuario?> AlterarUsuario(Usuario usuario);
}

public interface IUsuarioService : IService<Usuario>, IUsuarioInteraction
{
}