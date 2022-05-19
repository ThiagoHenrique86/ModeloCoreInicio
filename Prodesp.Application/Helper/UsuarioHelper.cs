using Prodesp.Application.CrossCutting.TO.Request.Usuario;
using Prodesp.Application.CrossCutting.TO.Response.Usuario;
using Prodesp.Application.Validation;
using Prodesp.Domain.Shared.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Prodesp.Application.Helper;

public static class UsuarioAppServiceHelper
{

   
    public static UsuarioListResponse? ToListResponse(this UsuarioListValidationResult list)
    {
        return new UsuarioListResponse
        {
            Data = list.Data.Records.Select(s => s.ToSingleResponse()).ToList(),
            TotalPages = list.Data.Page.TotalPages,
            TotalRecords = list.Data.Page.TotalRecords
        };
    }
    public static UsuarioResponseData? ToSingleResponse(this Usuario Usuario)
    {
        if (Usuario == null)
        {
            return null;
        }

        var UsuarioTO = new UsuarioResponseData
        {
            IdUsuario = Usuario.IdUsuario,
            NomeUsuario = Usuario.NomeUsuario,
            Ativo = Usuario.Ativo,
            DataInclusao = Usuario.DataInclusao,
            DataAlteracao = Usuario.DataAlteracao
         
        };

        return UsuarioTO;
    }

    
    public static ResetarSenhaUsuarioResponseData? ToSingleResponse(this ResetarSenhaUsuarioResponseData resetarSenha)
    {
        if (resetarSenha == null)
        {
            return null;
        }

        var UsuarioTO = new ResetarSenhaUsuarioResponseData
        {
            Login = resetarSenha.Login,
            SenhaProvisoria = resetarSenha.SenhaProvisoria,
            TextoRetorno = resetarSenha.TextoRetorno
        };

        return UsuarioTO;
    }


    public static UsuarioLoginResponseData? ToSingleLoginResponse(this Usuario Usuario)
    {
        if (Usuario == null)
        {
            return null;
        }

        var UsuarioTO = new UsuarioLoginResponseData
        {
            IdUsuario = Usuario.IdUsuario,
            NomeUsuario = Usuario.NomeUsuario
           
        };

        return UsuarioTO;
    }



    public static string GerarSaltkey(int tamanho)
    {
        Random rand = new Random();
        string key = rand.Next(0, int.MaxValue).ToString();

        MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] input = System.Text.Encoding.ASCII.GetBytes(key);
        byte[] hashCode = md5.ComputeHash(input);

        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < hashCode.Length; i++)
            builder.Append(hashCode[i].ToString("x3"));

        if (tamanho > 16) tamanho = 16;

        return (builder.ToString().Substring(0, tamanho)).ToUpper();
    }

   
    public static Usuario? ToEntity(UsuarioRequest request)
    {
        if (request == null)
            return null;

        var p = request.Data;

        

        var usuario = new Usuario
        {
            IdUsuario = p.IdUsuario,
            NomeUsuario = p.NomeUsuario,
            Ativo = 1,
            IdUsuarioAlteracao = p.IdUsuarioAlteracao,
            IdUsuarioInclusao = p.IdUsuarioInclusao
            

        };

        return usuario;
    }

    public static Usuario? ToUsuario(UsuarioRequest request)
    {
        if (request == null)
            return null;

        var p = request.Data;

        var guid = Guid.NewGuid().ToString();

        var usuario = new Usuario
        {
            IdUsuario = guid
            ,
            NomeUsuario = p.NomeUsuario
            ,
            Ativo = 1
            ,
            DataInclusao = DateTime.Now
            ,
            DataAlteracao = null
            ,
            CodigoSaltKey = GerarSaltkey(8)
        };

        return usuario;
    }

    public static bool StrEmpty(string valor)
    {
        if (string.IsNullOrEmpty(valor))
            return true;

        if (!string.IsNullOrEmpty(valor) && valor?.ToLower()?.Trim() == "string")
            return true;

        return false;
    }

    

    

    

    public static bool ConfereSenha(string senha, string confirmacaoSenha)
    {
        return senha == confirmacaoSenha;
    }


}
