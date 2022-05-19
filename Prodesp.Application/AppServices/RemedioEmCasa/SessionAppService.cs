
using Prodesp.Application.Validation;
using Prodesp.Core.Backend.Application.Implementations;
using Prodesp.Core.Backend.Application.Interfaces;
using Prodesp.Core.Backend.Domain.Validations;
using Prodesp.Domain.Services.Interfaces;

namespace Prodesp.Application.AppServices;

public interface ISessaoAppService : IAppService<Domain.Shared.Entities.Sessao>
{
    Task<SessaoListValidationResult?> GetAll();
    Task<SessaoValidationResult?> Get(string idSessao);
    Task<SessaoValidationResult?> FindByAccessToken(string accessToken);
    Task<SessaoValidationResult?> GetLastActiveSession(string idUsuario);
 
}

public class SessaoAppService : AppService<Domain.Shared.Entities.Sessao, ISessaoService>, ISessaoAppService
{
    private readonly IUsuarioService? _UsuarioService;

    public SessaoAppService(ISessaoService service,
                            IUsuarioService IUsuarioService)
        : base(service)
    {
        _UsuarioService = IUsuarioService;
    }

    public async Task<SessaoValidationResult?> FindByAccessToken(string accessToken)
    {
        SessaoValidationResult? result = new SessaoValidationResult();
        result.Data = await _service.FindByAccessToken(accessToken);
        return result;
    }

    public async Task<SessaoValidationResult?> Get(string idSessao)
    {
        SessaoValidationResult? result = new SessaoValidationResult();
        result.Data = await _service.GetSessao(idSessao);
        return result;
    }

    public async Task<SessaoListValidationResult?> GetAll()
    {
        SessaoListValidationResult? result = new SessaoListValidationResult();
        result.Data = await _service.GetAll();
        return result;
    }

    public async Task<SessaoValidationResult?> GetLastActiveSession(string idUsuario)
    {
        SessaoValidationResult? result = new SessaoValidationResult();
        var lastActiveSession = await _service
            .FindAsync(x => x.IdUsuario == idUsuario && x.DataValidadeAccessToken >= DateTime.Now);
           
            
        result.Data = lastActiveSession.OrderByDescending(x => x.DataInclusao).FirstOrDefault();

        return result;
    }




    #region Validate
    public override ValidationResult ValidateDelete(Domain.Shared.Entities.Sessao entity)
    {
        throw new NotImplementedException();
    }

    public override ValidationResult ValidateInsert(Domain.Shared.Entities.Sessao entity)
    {
        throw new NotImplementedException();
    }

    public override ValidationResult ValidateUpdate(Domain.Shared.Entities.Sessao entity)
    {
        throw new NotImplementedException();
    }
    #endregion
}
