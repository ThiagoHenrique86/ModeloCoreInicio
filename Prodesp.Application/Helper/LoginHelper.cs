
using Prodesp.Application.CrossCutting.TO.Response.Login;
using Prodesp.Application.Validation;
using Prodesp.Domain.Services.Interfaces;
using Prodesp.Domain.Shared.Entities;

namespace Prodesp.Application.Helper;

public static class LoginAppServiceHelper
{

    public static string CriptoPassword(string password, string saltKey)
    {
        var cripto = new Prodesp.Gsnet.Framework.HelperCrypt();
        cripto.Key = saltKey;

        return cripto.Encrypt(password);
    }

    public static string validarAcesso(string accessToken, ILoginService service)
    {
        var login = service.GetLoginByToken(accessToken);
        if (login == null)
            return $"Login não encontrado pra esse token {accessToken}";

        return string.Empty;
    }


    public static LoginListResponse ToListResponse(this LoginListValidationResult list)
    {
        return new LoginListResponse
        {
            Data = list.Data.Records.Select(s => s.ToSingleResponse()).ToList(),
            TotalPages = list.Data.Page.TotalPages,
            TotalRecords = list.Data.Page.TotalRecords
        };
    }


    public static LoginResponseData ToSingleResponse(this Login login)
    {
        if (login == null)
        {
            return null;
        }
        var response = new LoginResponseData
        {
            FlagBloqueado = login.FlagBloqueado,
            FlagAtivo = login.FlagAtivo,
            PrimeiroAcesso = login.PrimeiroAcesso,
            CodigoLogin = login.CodigoLogin,
            //Sessao = login.SessaoAtiva.ToSingleResponse(),
            Usuario = login.Usuario.ToSingleLoginResponse()//,

        };



        return response;
    }


    

    private static Random random = new Random();

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }


    public static Login? ToLogin(Usuario usuario)
    {
        if (usuario == null)
            return null;

        var login = new Login
        {
            IdLogin = Guid.NewGuid().ToString(),
            IdUsuario = usuario.IdUsuario,
            CodigoLogin = usuario.NomeUsuario,
            CodigoSenha = CriptoPassword(RandomString(8), usuario.CodigoSaltKey)
            ,
            DataValidade = DateTime.Now.AddYears(2)
            ,
            FlagBloqueado = 0
            ,
            FlagAtivo = 1
            ,
            NumeroTentativaLogin = 0
            ,
            DataInclusao = DateTime.Now
            ,
            DataAlteracao = null
            ,
            IdUsuarioInclusao = usuario?.IdUsuario // vai ser setado no momento da gravação do vacinador
            ,
            IdUsuarioAlteracao = null
        };

        return login;
    }

   
}