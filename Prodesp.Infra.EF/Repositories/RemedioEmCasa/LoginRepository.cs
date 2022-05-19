

using Microsoft.EntityFrameworkCore;
using Prodesp.Domain.Repositories.Interfaces;
using Prodesp.Domain.Shared.Entities;
using Prodesp.Infra.EF;
using static Prodesp.Infra.EF.UnitOfWorkCore.IUnitOfWork;

public class LoginRepository : Repository<Login, RemedioEmCasaContexto>, ILoginRepository
{
    public LoginRepository(IUnitOfWork<RemedioEmCasaContexto> uow) :
        base(uow)
    {

    }

    public Login? GetLogin(string login)
    {
        var linq = UnityOfWork.Contexto.Login.Where(l => l.CodigoLogin == login && l.FlagAtivo == 1).Include(u => u.Usuario).AsNoTracking();

        return linq.FirstOrDefault();
    }

    public async Task<Login?> GetLoginAsync(string login)
    {
        var linq =  UnityOfWork.Contexto.Login.Where(l => l.CodigoLogin == login && l.FlagAtivo == 1).Include(u => u.Usuario).AsNoTracking();

        return await linq.FirstOrDefaultAsync();
    }

    public override void Delete(Login entity)
    {
        try
        {
            var entidade = this.UnityOfWork.Contexto.Login.Where(x => x.IdLogin == entity.IdLogin);
            this.UnityOfWork.Contexto.Login.RemoveRange(entidade);
            this.UnityOfWork.Contexto.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            //this.UnityOfWork.Contexto.Configuration.ValidateOnSaveEnabled = oldValidateOnSaveEnabled;
        }
    }
    public async Task<Login?> GetLoginByToken(string token)
    {
        var linq = from Usuario in UnityOfWork.Contexto.Usuario
                   join login in UnityOfWork.Contexto.Login on Usuario.IdUsuario equals login.IdUsuario
                   join sessao in UnityOfWork.Contexto.Sessao on login.IdLogin equals sessao.IdLogin
                   //join perfil in UnityOfWork.Contexto.Perfil on Usuario.IdPerfil equals perfil.IdPerfil
                   where
                      sessao.CodigoAccessToken == token &&
                      DateTime.Now < sessao.DataValidadeAccessToken
                   select login;

        return await linq.FirstOrDefaultAsync();
    }
}
