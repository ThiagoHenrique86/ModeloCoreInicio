
using Microsoft.EntityFrameworkCore;
using Prodesp.Domain.Repositories.Interfaces;
using Prodesp.Domain.Shared.Entities;
using static Prodesp.Infra.EF.UnitOfWorkCore.IUnitOfWork;

namespace Prodesp.Infra.EF.Repositories;

public class UsuarioRepository : Repository<Usuario, RemedioEmCasaContexto>, IUsuarioRepository
{
    public UsuarioRepository(IUnitOfWork<RemedioEmCasaContexto> uow) :
        base(uow)
    {

    }

    public async Task<Usuario?> GetByUsuarioID(string IdUsuario)
    {
        var linq = UnityOfWork.Contexto.Usuario.Where(l => l.IdUsuario == IdUsuario && l.Ativo == 1).AsNoTracking();

        return await linq.FirstOrDefaultAsync();
    }

    public async Task<Usuario?> GetUsuarioNome(string nome)
    {
        if (!string.IsNullOrEmpty(nome))
        {
            var ctx = UnityOfWork.Contexto;
            string msg = string.Empty;
            var Usuario = new Usuario();

            var ctx_Usuario = UnityOfWork.Contexto.Set<Usuario>();

            try
            {
                var linq = (from p in ctx_Usuario
                            where
                                //d.NumeroDocumento.Replace(".", "").Replace("-", "") == cpf
                                p.NomeUsuario == nome
                                && p.Ativo == 1
                            select p
                    );

                var retorno_Usuario = await linq.FirstOrDefaultAsync();

                //if (retorno_Usuario != null && string.IsNullOrEmpty(retorno_Usuario?.IdEstabelecimentoReferencia))
                //{
                //    msg = @"Unidade de Referência não informada para o Usuário solicitado";
                //    Usuario.ValidationResult.Add(msg);
                //}

                if (retorno_Usuario == null)
                {
                    msg = "Usuário não localizado";
                    Usuario.ValidationResult.Add(msg);
                }
                else if (retorno_Usuario != null)
                    return retorno_Usuario;

                return Usuario;
            }
            catch (System.Exception ex)
            {
                msg = $"Erro: {ex.Message} ";
                Usuario.ValidationResult.Add(msg);
            }

        }

        return null;
    }

    public async Task<Usuario?> IncluirUsuario(Usuario usuario, Login login)
    {
        if (usuario == null || login == null)
            return null;

        UnityOfWork.Contexto.Entry(usuario).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        await UnityOfWork.Contexto.SaveChangesAsync();

        UnityOfWork.Contexto.Entry(login).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        await UnityOfWork.Contexto.SaveChangesAsync();

        return usuario;
    }


    public async Task<Usuario?> AlterarUsuario(Usuario usuario)
    {
        if (usuario == null)
            return null;

        UnityOfWork.Contexto.Entry(usuario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        await UnityOfWork.Contexto.SaveChangesAsync();

        return usuario;
    }
}

