using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Prodesp.Application.AppServices;
using Prodesp.Application.CrossCutting.TO.Request.Login;
using Prodesp.Application.CrossCutting.TO.Response.Login;
using Prodesp.Application.Helper;
using Prodesp.Application.Validation;
using Prodesp.Infra.EF;
using static Prodesp.Infra.EF.UnitOfWorkCore.IUnitOfWork;


namespace Prodesp.Usuario.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    public ILoginAppService _service { get; set; }
    public IUnitOfWork<RemedioEmCasaContexto> _unitOfWork { get; set; }
    public LoginController(ILoginAppService service,
                           IUnitOfWork<RemedioEmCasaContexto> unitOfWork)

    {
        this._service = service;
        this._unitOfWork = unitOfWork;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Logar(LoginRequest request)
    {
        var response = new LoginSingleResponse() { DtStart = DateTime.Now, RequestToken = Guid.NewGuid().ToString() };
        try
        {
            var _userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

            var _ip = HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress;

            var result = await _service.Logar(request, _ip.ToString(), _userAgent);

            if (result.IsValid)
            {
                if (result.Data != null )
                {
                    var saveResponse = await _unitOfWork.SaveAsync();
                    if (saveResponse != null && saveResponse.Errors.Count<Core.Backend.Domain.Validations.ValidationError>() > 0)
                    {
                        throw new ApplicationException("Erro ao persistir os dados");
                    }
                    if(response.ValidationSummary != null)
                    {
                        var handlingError = new HandlingErrorsHelper<LoginSingleResponse, LoginValidationResult>();
                        response = handlingError.FillValidationError(response, result, "Erro ao logar no sistema", "");
                        return BadRequest(response);
                    }
                    else
                    {
                        response.Data = result.Data.SessaoAtiva.CodigoAccessToken;
                    }
                    
                }

                return Ok(response);
            }
            else
            {
                var handlingError = new HandlingErrorsHelper<LoginSingleResponse, LoginValidationResult>();
                response = handlingError.FillValidationError(response, result, "Erro ao logar no sistema", "");
                return BadRequest(response);
            }
        }
        catch (Exception ex)
        {

            var handlingError = new HandlingErrorsHelper<LoginSingleResponse, LoginListValidationResult>();
            response = handlingError.FillValidationError(response, null, "Erro ao logar no sistema", ex.Message);
            return BadRequest(response);
        }
    }

}