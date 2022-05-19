using Prodesp.Core.Backend.Domain.Interfaces.Infra.Data.Repository;
using Prodesp.Domain.Shared.Entities;

namespace Prodesp.Domain.Repositories.Interfaces;

    public interface IUsuarioRepository : IRepository<Shared.Entities.Usuario>
    {
        Task<Usuario?> GetByUsuarioID(string IdUsuario);
        Task<Usuario?> GetUsuarioNome(string nome);
        Task<Usuario?> IncluirUsuario(Usuario usuario, Login login);
        Task<Usuario?> AlterarUsuario(Usuario usuario);
    }

