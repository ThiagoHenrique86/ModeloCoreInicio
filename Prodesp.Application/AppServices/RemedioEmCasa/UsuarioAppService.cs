using AutoMapper;
using Prodesp.Application.CrossCutting.TO.Request.Usuario;
using Prodesp.Application.CrossCutting.TO.Response.Usuario;
using Prodesp.Application.Helper;
using Prodesp.Application.Validation;
using Prodesp.Core.Backend.Application.Implementations;
using Prodesp.Core.Backend.Application.Interfaces;
using Prodesp.Core.Backend.Domain.Models;
using Prodesp.Core.Backend.Domain.Validations;
using Prodesp.Domain.Services.Interfaces;
using Prodesp.Domain.Shared.Entities;
using System.Data.Entity;

namespace Prodesp.Application.AppServices;


public interface IUsuarioAppService : IAppService<Domain.Shared.Entities.Usuario>
{
    Task<UsuarioDataListValidationResult> GetAll();
    Task<LoginValidationResult> GetUsuarioPorLogin(string cdLogin);

    Task<UsuarioValidationResult> Get(string idUsuario);
    
    Task<UsuarioValidationResult> InserirUsuario(UsuarioRequest usuario);
    Task<UsuarioValidationResult> AlterarUsuario(UsuarioRequest usuario);

    Task<UsuarioValidationResult> CriarDadosUsuario(Usuario usuario, Login login);
   

    Task<AlterarSenhaUsuarioResult> AlterarSenhaUsuario(AlterarSenhaUsuarioRequest request);

    Task<ResetarSenhaUsuarioResult> ResetarSenhaUsuario(string login);

}

public class UsuarioAppService : AppService<Usuario, IUsuarioService>, IUsuarioAppService
{
    //private readonly IEstabelecimentoAppService _EstabelecimentoAppService;   
    private readonly ILoginService _LoginService;
    private readonly ISessaoService _SessaoService;
    private readonly IMapper _Mapper;

    public UsuarioAppService(IUsuarioService service,
                            ILoginService ILoginService,
                            ISessaoService ISessaoService,
                            IMapper mapper
                            )
        : base(service)
    {
        //_EstabelecimentoAppService = IEstabelecimentoAppService;
        _LoginService = ILoginService;
        _SessaoService = ISessaoService;
        _Mapper = mapper;
    }

    public async Task<AlterarSenhaUsuarioResult> AlterarSenhaUsuario(AlterarSenhaUsuarioRequest request)
    {
        ValidationResult errors = new ValidationResult();
        AlterarSenhaUsuarioResult result = new AlterarSenhaUsuarioResult();

        if (request == null || request.Data == null)
            errors.Add(new ValidationError("Requisição inválida"));
        else
        {
            if (string.IsNullOrEmpty(request.Data.SenhaAtual))
                errors.Add(new ValidationError("Senha atual não informada"));

            if (string.IsNullOrEmpty(request.AccessToken))
                errors.Add(new ValidationError("AccessToken da sessão é obrigatório"));

            if (!UsuarioAppServiceHelper.ConfereSenha(request.Data.NovaSenha, request.Data.ConfirmacaoSenha))
                errors.Add(new ValidationError("Senha e a confirmação da senha não conferem"));

            if (!errors.Errors.Any())
            {
                
                var sessaoLogin = SessaoAppServiceHelper.ValidateTokenJWT(request.AccessToken);
                //var sessao = _SessaoService.Find(x => x.CodigoAccessToken == accessToken).FirstOrDefault();
                if (sessaoLogin == null)
                    errors.Add(new ValidationError("Token inválido. Por favor faça o login novamente!"));
                else
                {
                    var login = await _LoginService.GetLoginAsync(sessaoLogin);
                    if (login == null)
                        errors.Add(new ValidationError("Login desta sessão não localizada"));
                    else
                    {
                        if (login.Usuario == null)
                            errors.Add(new ValidationError("Usuário não localizado para este login"));
                        else
                        {
                            if (string.IsNullOrEmpty(login.Usuario.CodigoSaltKey))
                                errors.Add(new ValidationError("Saltkey do usuário não está cadastrado. É necessário ter um saltkey"));
                            else
                            {
                                var saltKey = login.Usuario.CodigoSaltKey;
                                var criptSenhaAtual = LoginAppServiceHelper.CriptoPassword(request.Data.SenhaAtual, saltKey);
                                if (string.IsNullOrEmpty(criptSenhaAtual))
                                    errors.Add(new ValidationError("Ocorreu um problema na criptografia da senha atual"));

                                var criptSenhaNova = LoginAppServiceHelper.CriptoPassword(request.Data.NovaSenha, saltKey);
                                if (string.IsNullOrEmpty(criptSenhaNova))
                                    errors.Add(new ValidationError("Ocorreu um problema na criptografia da nova senha"));

                                if (criptSenhaAtual != login.CodigoSenha)
                                    errors.Add(new ValidationError("A senha atual está incorreta"));

                                if (criptSenhaAtual == criptSenhaNova)
                                    errors.Add(new ValidationError("A nova senha não pode ser a mesma que a senha atual"));

                                if (!errors.Errors.Any())
                                    errors = await _service.AtualizarSenha(request.AccessToken, criptSenhaAtual, criptSenhaNova);
                            }
                        }
                    }
                }

            }

            result.Data = new CrossCutting.TO.Response.Usuario.AlterarSenhaUsuarioResponseData
            {
                Sucesso = !errors.Errors.Any()
            };
            result.Add(errors);
        }


        return result;
    }



    public async Task<ResetarSenhaUsuarioResult> ResetarSenhaUsuario(string loginParaResetar)
    {
        ValidationResult errors = new ValidationResult();
        ResetarSenhaUsuarioResult result = new ResetarSenhaUsuarioResult();

        if (loginParaResetar == null)
            errors.Add(new ValidationError("Login não informado"));
        else
        {
            var NovaSenha = UsuarioAppServiceHelper.GerarSaltkey(5);

            var login = await _LoginService.Where(x => x.CodigoLogin == loginParaResetar)
                                     .Include(U => U.Usuario)
                                     .FirstOrDefaultAsync();

            if (login == null)
                errors.Add(new ValidationError("Login não localizado"));
            else
            {
                if (login.Usuario == null)
                    errors.Add(new ValidationError("Usuário não localizado para este login"));
                else
                {
                    if (string.IsNullOrEmpty(login?.Usuario?.CodigoSaltKey))
                        errors.Add(new ValidationError("Saltkey do usuário não está cadastrado. É necessário ter um saltkey"));
                    else
                    {
                        var saltKey = login.Usuario.CodigoSaltKey;

                        var criptSenhaNova = LoginAppServiceHelper.CriptoPassword(NovaSenha, saltKey);
                        if (string.IsNullOrEmpty(criptSenhaNova))
                            errors.Add(new ValidationError("Ocorreu um problema na criptografia da nova senha"));

                        if (!errors.Errors.Any())
                            await _service.ResetarSenha(login, criptSenhaNova);
                    }
                }
            }

            result.Data = new CrossCutting.TO.Response.Usuario.ResetarSenhaUsuarioResponseData
            {
                Login = loginParaResetar,
                SenhaProvisoria = NovaSenha,
                TextoRetorno = "Senha: " + NovaSenha + " provisória para o login: " + loginParaResetar,

            };
            result.Add(errors);
        }

        return result;
    }



    public async Task<UsuarioValidationResult> AlterarUsuario(UsuarioRequest usuarioRequest)
    {
        var usuario = UsuarioAppServiceHelper.ToEntity(usuarioRequest);

        UsuarioValidationResult result = new UsuarioValidationResult();
        usuario.DataAlteracao = DateTime.Now;
        var usuarioReq = await this.Get(usuario.IdUsuario);
        if (usuarioReq != null)
        {
            usuario.CodigoSaltKey = usuarioReq.Data.CodigoSaltKey;
            usuario.IdUsuarioAlteracao = SessaoAppServiceHelper.TokenJWTRetornaIdUsuario(usuarioRequest.AccessToken);

            if (usuario.IsValid)
            {
                result.Data = await _service.AlterarUsuario(usuario);
            }
            else
            {
                usuario.ValidationResult.Errors.ToList().ForEach(x => result.Add(x.Message));
            }
        }
        else
        {
            result.Add("Usuário não encontrado");
        }

        return result;
    }

    public async Task<UsuarioValidationResult> CriarDadosUsuario(Usuario usuario, Login login)
    {
        UsuarioValidationResult result = new UsuarioValidationResult();
        //Incluir Pessoa
        if (usuario.IsValid && login.IsValid)
        {
            result.Data = await _service.IncluirUsuario(usuario, login);
        }
        return result;
    }

   

    public async Task<UsuarioValidationResult> Get(string idUsuario)
    {
        UsuarioValidationResult result = new UsuarioValidationResult();
        result.Data = await  _service.GetUsuario(idUsuario);
        return result;
    }

    public async Task<UsuarioDataListValidationResult> GetAll()
    {
        UsuarioDataListValidationResult result = new UsuarioDataListValidationResult();
        var pageResultUsuario = await _service.GetAll();
        
        var pageResultUsuarioResponseData = new PageResult<UsuarioResponseData>(); 
        this._Mapper.Map(pageResultUsuario, pageResultUsuarioResponseData);
        result.Data = pageResultUsuarioResponseData;

        return result;
    }

    public async Task<LoginValidationResult> GetUsuarioPorLogin(string cdLogin)
    {
        LoginValidationResult result = new LoginValidationResult();
        result.Data = await _service.GetUsuarioPorLogin(cdLogin);
        return result;
    }

    public async Task<UsuarioValidationResult> InserirUsuario(UsuarioRequest usuarioRequest)
    {
        var usuario = UsuarioAppServiceHelper.ToEntity(usuarioRequest);
        UsuarioValidationResult result = new UsuarioValidationResult();

        //DateTime? DtAlteracao = DateTime.Now;
        usuario.IdUsuario = Guid.NewGuid().ToString();
        usuario.DataInclusao = DateTime.Now;
        if (!string.IsNullOrEmpty(usuarioRequest.AccessToken))
        {
            usuario.IdUsuarioInclusao = SessaoAppServiceHelper.TokenJWTRetornaIdUsuario(usuarioRequest.AccessToken);

        }

        usuario.CodigoSaltKey = UsuarioAppServiceHelper.GerarSaltkey(8);
        //Incluir Usuário
        if (usuario.IsValid)
        {
          var loginExiste=  await this._LoginService.GetLoginAsync(usuario.NomeUsuario);
            
            if(loginExiste != null)
            {
                result.Add("Login já existente");
            }

           var login = LoginAppServiceHelper.ToLogin(usuario);

            if(login != null)
            {
                result.Data = await _service.IncluirUsuario(usuario, login);

            }
        }
        else
        {
            usuario.ValidationResult.Errors.ToList().ForEach(x => result.Add(x.Message));
        }
        return result;
    }

    

    public override ValidationResult ValidateDelete(Usuario entity)
    {
        throw new NotImplementedException();
    }

    public override ValidationResult ValidateInsert(Usuario entity)
    {
        throw new NotImplementedException();
    }

    public override ValidationResult ValidateUpdate(Usuario entity)
    {
        throw new NotImplementedException();
    }
}