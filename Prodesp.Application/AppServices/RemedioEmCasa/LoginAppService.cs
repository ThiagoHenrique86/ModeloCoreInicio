using Prodesp.Application.CrossCutting.TO.Request.Login;
using Prodesp.Application.Helper;
using Prodesp.Application.Validation;
using Prodesp.Core.Backend.Application.Implementations;
using Prodesp.Core.Backend.Application.Interfaces;
using Prodesp.Core.Backend.Domain.Validations;
using Prodesp.Domain.Services.Interfaces;
using Prodesp.Domain.Shared.Entities;
using Microsoft.Extensions.Configuration;

namespace Prodesp.Application.AppServices;


public interface ILoginAppService : IAppService<Login>
{
    Task<LoginListValidationResult> GetAll();
    Task<LoginValidationResult> Get(string idLogin);
    Task<LoginValidationResult> Logar(LoginRequest request, string ip, string userAgent);
    
    Task<Login?> Login(string idLogin);
}

public class LoginAppService : AppService<Domain.Shared.Entities.Login, ILoginService>, ILoginAppService
{

    private readonly ITokenHelperAppService _TokenHelperAppService;
    private readonly IConfiguration _configuration;
    public LoginAppService(ILoginService service,
                           ITokenHelperAppService tokenHelperAppService,
                           IConfiguration configuration


        ) : base(service)
    {
        _TokenHelperAppService = tokenHelperAppService;
        _configuration = configuration;
    }


    public async Task<LoginValidationResult> Logar(LoginRequest request, string ip, string userAgent)
    {
        LoginValidationResult result = new LoginValidationResult();
       
        if (request == null || request.Data == null)
            result.Add("Dados do login incorretos");
        else
        {
            try
            {
                if (string.IsNullOrEmpty(request.Data?.Login))
                    result.Add("Erro ao logar. Login não informado");
                else
                {
                    var _login = await _service.GetLoginAsync(request.Data.Login);
                    
                    if (_login == null)
                    {
                        result.Add("Usuário não localizado");
                    }
                    else
                    {
                        var validacao = ValidarLogin(_login);
                        
                        if (validacao.Errors?.Count() == 0)
                        {
                            var senhaCriptografada = LoginAppServiceHelper.CriptoPassword(request.Data.Senha, _login.Usuario.CodigoSaltKey);

                            if (senhaCriptografada == _login.CodigoSenha)
                            {
                               this.PrimeiroAcesso(_login);

                                _login.SessaoAtiva = await this._TokenHelperAppService.GerarToken(_login, ip, userAgent);

                               var zerarTentativasComSucesso = await this._service.ZerarTentativasLogin(_login);
                               
                                if (!zerarTentativasComSucesso)
                                    throw new ApplicationException("Não foi possível zerar a quantidade de tentativas de login");
                                
                                result.Data = _login;
                            }
                            else
                            {
                                result.Add("Login ou senha inválido");
                                if (_login.FlagBloqueado == 1)
                                {
                                    result.Add("Login bloqueado por excesso de tentativas");
                                }
                            }
                            result.Data = _login;
                        }
                        else
                        {
                            validacao?.Errors?.ToList().ForEach(err =>
                            {
                                result.Add(err.Message);
                            });
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
                
            }
        }

        return result;
    }




    private ValidationResult ValidarLogin(Login _login)
    {
        var validacoes = new Core.Backend.Domain.Validations.ValidationResult();

        if (_login == null)
            validacoes.Add("Login ou senha inválido");
        else if (_login?.FlagBloqueado == 1)
        {
            validacoes.Add("Login bloqueado");
        }
        else if (_login?.FlagAtivo == 0)
        {
            validacoes.Add("Login inativo");
        }
        else if (_login?.Usuario == null)
        {
            validacoes.Add("Nenhum Usuário vinculado a este login");
        }

        return validacoes;
    }

    private void PrimeiroAcesso(Login _login)
    {
        if (_login.DataAlteracao == null)
        {
            _login.PrimeiroAcesso = true;
        }
    }


 

    public async Task<Login?> Login(string idLogin)
    {
        var login = await _service.Login(idLogin);

        return login;
    }

    

    public LoginListValidationResult GetAll()
    {
        throw new NotImplementedException();
    }

    public override ValidationResult ValidateDelete(Login entity)
    {
        throw new NotImplementedException();
    }

    public override ValidationResult ValidateInsert(Login entity)
    {
        throw new NotImplementedException();
    }

    public override ValidationResult ValidateUpdate(Login entity)
    {
        throw new NotImplementedException();
    }

    Task<LoginListValidationResult> ILoginAppService.GetAll()
    {
        throw new NotImplementedException();
    }

    Task<LoginValidationResult> ILoginAppService.Get(string idLogin)
    {
        throw new NotImplementedException();
    }
}