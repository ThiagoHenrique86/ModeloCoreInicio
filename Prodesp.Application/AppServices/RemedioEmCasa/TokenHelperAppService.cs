using Microsoft.Extensions.Configuration;
using Prodesp.Application.Helper;
using Prodesp.Domain.Services.Interfaces;
using Prodesp.Domain.Shared.Entities;

namespace Prodesp.Application.AppServices;
public class TokenHelperAppService: ITokenHelperAppService
{
    private readonly ILoginService _LoginService;
    private readonly IConfiguration _configuration;
    public TokenHelperAppService(ILoginService service,
                                 IConfiguration configuration)
    {
        _LoginService = service;
        _configuration = configuration;
    }

    public async Task<Sessao?> GerarToken(Login _login, string ip, string userAgent)
    {
        var TempoDuracaoTokenHoras = this._configuration.GetSection("AppSettings").GetSection("JWT.EXPIRATION.HOURS").Value ?? "8";  // **ConfigurationManager.AppSettings["JWT.EXPIRATION.HOURS"];
        var payloadData = _login.ToSingleResponse();

        var TokenJWT = SessaoAppServiceHelper.generateJwtToken(payloadData, TempoDuracaoTokenHoras);

        int tempoExpTkp = 0;
        if (int.TryParse(TempoDuracaoTokenHoras, out tempoExpTkp))
            return await this._LoginService.GerarSessao(ip, _login, userAgent, tempoExpTkp, TokenJWT);
        else
            return null;
    }
}

public interface ITokenHelperAppService
{
    public Task<Sessao?> GerarToken(Login _login, string ip, string userAgent);
}
