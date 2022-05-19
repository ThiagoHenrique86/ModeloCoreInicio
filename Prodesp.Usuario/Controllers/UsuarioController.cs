using Microsoft.AspNetCore.Mvc;
using Prodesp.Application.AppServices;
using Prodesp.Application.CrossCutting.TO.Request.Usuario;
using Prodesp.Application.CrossCutting.TO.Response.Usuario;
using Prodesp.Application.Helper;
using Prodesp.Application.Validation;
using Prodesp.Gsnet.Core.TO.Validation;
using Prodesp.Infra.EF;
using WebApi.Intercept;
using static Prodesp.Infra.EF.UnitOfWorkCore.IUnitOfWork;

namespace Prodesp.Usuario.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    public IUsuarioAppService _service { get; set; }
    public IUnitOfWork<RemedioEmCasaContexto> _unitOfWork { get; set; }
    public UsuarioController(IUsuarioAppService service,
                           IUnitOfWork<RemedioEmCasaContexto> unitOfWork)

    {
        this._service = service;
        this._unitOfWork = unitOfWork;
    }

    [HttpPost("alterar-senha")]
    [Authorize]
    public async Task<IActionResult> AlterarSenhaUsuario(AlterarSenhaUsuarioRequest request)
    {
        var response = new AlterarSenhaUsuarioSingleResponse() { DtStart = DateTime.Now, RequestToken = Guid.NewGuid().ToString() };

        try
        {
            var alteracaoSenha = await _service.AlterarSenhaUsuario(request);

            var erros_lista = new List<FieldErrorData>();

            if (alteracaoSenha.Data.Sucesso)
            {
                var resultSave = await _unitOfWork.SaveAsync();
                if (resultSave.IsValid)
                {
                    response.Message = "Senha alterada com sucesso";
                    response.Data = new AlterarSenhaUsuarioResponseData
                    {
                        Sucesso = alteracaoSenha.Data.Sucesso
                    };
                    return Ok(response);
                }
                else
                    throw new ApplicationException(resultSave.Errors.FirstOrDefault().Message);
            }
            else
            {
                alteracaoSenha.Errors.ToList().ForEach(x =>
                {
                    erros_lista.Add(new FieldErrorData { ErrorMessage = x.Message });
                });

                response.ResultCode = 0;
                response.ValidationSummary = new ValidationData((int)System.Net.HttpStatusCode.BadRequest, "Erro ao tentar alterar a senha do usuário");
                response.ValidationSummary.Erros = erros_lista;
                return BadRequest(response);
            }
        }
        catch (Exception ex)
        {
            var handlingError = new HandlingErrorsHelper<AlterarSenhaUsuarioSingleResponse, AlterarSenhaUsuarioResult>();
            response = handlingError.FillValidationError(response, null, "Erro ao logar no sistema", ex.Message);
            return BadRequest(response);
        }
    }
     [HttpPost("Inserir-Usuario")]
    [Authorize]
    public async Task<IActionResult> InserirUsuario(UsuarioRequest usuarioRequest)
    {
        var response = new UsuarioSingleResponse() { DtStart = DateTime.Now, RequestToken = Guid.NewGuid().ToString() };
        try
        {
            
            using (var tran = this._unitOfWork.Contexto.Database.BeginTransaction())
            {
                var result = await this._service.InserirUsuario(usuarioRequest);
                if (result.IsValid)
                {
                    var saveResponse = await _unitOfWork.SaveAsync();
                    if (saveResponse != null && saveResponse.Errors.Any())
                    {
                        tran.Rollback();
                        throw new Exception("Erro ao persistir os dados");
                    }
                    else
                    {
                        tran.Commit();

                        response.Data = result.Data.ToSingleResponse();
                        response.Message = "Incluído com Sucesso!";
                        return Ok(response);
                    }

                }
                else
                {

                    tran.Rollback();
                    
                    var handlingError = new HandlingErrorsHelper<UsuarioSingleResponse, UsuarioValidationResult>();
                    response = handlingError.FillValidationError(response, result, "Erro ao logar no sistema", "");
                    return BadRequest(response);


                }

            }
        }
        catch (Exception ex)
        {

            var handlingError = new HandlingErrorsHelper<UsuarioSingleResponse, UsuarioListValidationResult>();
            response = handlingError.FillValidationError(response, null, "Erro ao logar no sistema", ex.Message);
            return BadRequest(response);
        }
    }

    [HttpPut("Alterar-Usuario")]
    [Authorize]
    public async Task<IActionResult> AlterarUsuario(UsuarioRequest usuarioRequest)
    {
        var response = new UsuarioSingleResponse() { DtStart = DateTime.Now, RequestToken = Guid.NewGuid().ToString() };
        try
        {
            var result = await this._service.AlterarUsuario(usuarioRequest);
            if (result.IsValid)
            {
                var saveResponse = await _unitOfWork.SaveAsync();
                if (saveResponse != null && saveResponse.Errors.Any())
                {
                    throw new Exception("Erro ao persistir os dados");
                }
                else
                {
                    response.Data = result.Data.ToSingleResponse();
                    response.Message = "Usuário alterado com Sucesso!";
                    return Ok(response);
                }

            }
            else
            {
                var handlingError = new HandlingErrorsHelper<UsuarioSingleResponse, UsuarioValidationResult>();

                response = handlingError.FillValidationError(response, result, "Erro ao logar no sistema", "");
                return BadRequest(response);

            }
        }
        catch (Exception ex)
        {

            var handlingError = new HandlingErrorsHelper<UsuarioSingleResponse, UsuarioListValidationResult>();
            response = handlingError.FillValidationError(response, null, "Erro ao logar no sistema", ex.Message);
            return BadRequest(response);
        }
    }
}