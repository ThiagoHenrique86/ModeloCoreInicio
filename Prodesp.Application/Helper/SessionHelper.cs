using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Prodesp.Application.CrossCutting.TO.Response.Login;
using Prodesp.Application.CrossCutting.TO.Response.Sessao;
using Prodesp.Application.Validation;
using Prodesp.Domain.Shared.Entities;
using Prodesp.Gsnet.Core.TO.Validation;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Serialization.Json;
using System.Security.Claims;
using System.Text;

namespace Prodesp.Application.Helper;

public static class SessaoAppServiceHelper
{

    public static string secretKey = "";

    private static IConfiguration _configuration;
    public static void SessaoAppServiceHelperConfigure(IConfiguration? configuration)
    {
        _configuration = configuration;
        secretKey = _configuration.GetSection("AppSettings").GetSection("Secret").Value;
    }

    public static SessaoListResponse ToListResponse(this SessaoListValidationResult list)
    {
        return new SessaoListResponse
        {
            Data = list.Data.Records.Select(s => s.ToSingleResponse()).ToList(),
            TotalPages = list.Data.Page.TotalPages,
            TotalRecords = list.Data.Page.TotalRecords
        };
    }

    public static SessaoResponseData ToSingleResponse(this Sessao sessao)
    {
        
        return new SessaoResponseData
        {
            IdAccessToken = sessao.IdAccessToken,
            CodigoAccessToken = sessao.CodigoAccessToken,
            QtdDuracaoAccessToken = sessao.QtdDuracaoAccessToken,
            DataValidadeAccessToken = sessao.DataValidadeAccessToken,
            IdUsuario = sessao.IdUsuario,
            IdLogin = sessao.IdLogin,
            NumeroIp = sessao.NumeroIp,
            NomeHost = sessao.NomeHost,
            DataInclusao = sessao.DataInclusao,
            FlagRefreshAccessToken = sessao.FlagRefreshAccessToken
        };
    }






    /*public static string generateJwtToken(LoginResponseData login, string TempoDuracaoTokenHoras)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);
        int tempoExpTkp = 1;
        int.TryParse(TempoDuracaoTokenHoras, out tempoExpTkp);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Hash, login.Usuario.IdUsuario),
                    new Claim(ClaimTypes.Name, login.CodigoLogin) }),
            Expires = DateTime.UtcNow.AddHours(tempoExpTkp),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        // var token = tokenHandler.CreateToken(tokenDescriptor);
        JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        //var token = handler.CreateToken(descriptor);

        token.Payload["Data"] = login;
        return tokenHandler.WriteToken(token);
    }
    */
    
    public static string generateJwtToken(LoginResponseData login, string TempoDuracaoTokenHoras)
    {
        byte[] key = Convert.FromBase64String(secretKey);

        int tempoExpTkp = 1;
        int.TryParse(TempoDuracaoTokenHoras, out tempoExpTkp);

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
        SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Hash, login.Usuario.IdUsuario),
                    new Claim(ClaimTypes.Name, login.CodigoLogin),
                    //new Claim(ClaimTypes.Role, login.Usuario.Perfil.CodigoPerfil)
                }),
            Expires = DateTime.UtcNow.AddHours(tempoExpTkp),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
        //var token = handler.CreateToken(descriptor);
        
        token.Payload["Data"] = login;
        return handler.WriteToken(token);
    }
    
    public static string? ValidateTokenJWT(string token)
    {
        string? login = null;
        ClaimsPrincipal? principal = GetTokenPrincipal(token);

        if (principal == null)
            return null;

        ClaimsIdentity? identity = null;
        try
        {
            identity = (ClaimsIdentity?)principal.Identity;
        }
        catch (NullReferenceException)
        {
            return null;
        }

        Claim? loginClaim = identity.FindFirst(ClaimTypes.Name);
        login = loginClaim.Value;

        return login;
    }


    private static ClaimsPrincipal? GetTokenPrincipal(string token)
    {
        try
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);

            if (jwtToken == null)
                return null;

            byte[] key = Convert.FromBase64String(secretKey);

            TokenValidationParameters parameters = new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            SecurityToken securityToken;
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out securityToken);
            return principal;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public static string? TokenJWTRetornaIdUsuario(string token)
    {
        try
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);

            if (jwtToken == null)
                return null;

            byte[] key = Convert.FromBase64String(secretKey);

            TokenValidationParameters parameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                RequireExpirationTime = false,
                ValidateLifetime = false,
                ValidateAudience = false
            };

            SecurityToken securityToken;
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out securityToken);

            string? idUsuario = null;

            if (principal == null)
                return null;

            ClaimsIdentity? identity = null;
            try
            {
                identity = (ClaimsIdentity?)principal.Identity;
            }
            catch (NullReferenceException)
            {
                return null;
            }

            Claim? idUsuarioClaim = identity?.FindFirst(ClaimTypes.Hash);
            idUsuario = idUsuarioClaim?.Value;

            return idUsuario;
        }
        catch
        {
            return null;
        }


    }
    //***fim JWT --------------------------

    public static LoginResponseData? TokenToLoginResponse(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);

        var data = jwtToken.Claims.FirstOrDefault(x => x.Type == "Data");

        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(LoginResponseData));
        MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(data.Value));
        var obj = (LoginResponseData?)serializer.ReadObject(ms);


        return obj;

    }
}
