using Microsoft.AspNetCore.Mvc;
using Prodesp.Application.AppServices;
using Prodesp.Application.AppServices.Medex;
using Prodesp.Application.CrossCutting.TO.Response.Medex.Paciente;
using Prodesp.Application.CrossCutting.TO.Response.Usuario;
using Prodesp.Application.Helper;
using Prodesp.Application.Validation;
using Prodesp.Application.Validation.Medex;
using Prodesp.Infra.EF.Helpers;
using System.Net;
using WebApi.Intercept;

namespace Prodesp.GVE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
        
    public IUsuarioAppService _UsuarioAppService { get; }
    public IPacienteAppService _PacienteAppService { get; }

    public TestController(IUsuarioAppService usuarioAppService, IPacienteAppService pacienteAppService)
                
    {
        this._UsuarioAppService = usuarioAppService;
        this._PacienteAppService = pacienteAppService;
    }

    [HttpPost("decrypt-string")]
    public IActionResult DecriptString([FromBody] string valorCript)
    {
        // = "Server=DESKTOP-7UJKAVE\\SQLEXPRESS;Database=teste;User Id=sa;Password=123456;MultipleActiveResultSets=true;";

        //string valorComCript = EncryptHelper.Encrypt(valorSemCript);
        var valorSemCript = ConnectionStringToken.Parse(valorCript).ConnectionString;
        // var saltKey = ConnectionStringToken.Parse(valorCript).Saltkey;
        //string valorDec = EncryptHelper.Decrypt(valorComCript);

        return Ok(valorSemCript );   

    }

    [HttpPost("crypt-string/{SaltKey}")]
    public IActionResult CriptString([FromBody] string valorCript, string SaltKey = "M1Z0N2X9")
    {
        var conToken = new ConnectionStringToken();
        conToken.ConnectionString = valorCript;
        conToken.Saltkey = SaltKey;

        return Ok(conToken.ToString());

    }


    [HttpGet("consulta-todos-usuarios")]
    public async Task<IActionResult> ConsultaTodosUsuarios()
    {
        var response = new UsuarioListResponse() { DtStart = DateTime.Now, RequestToken = Guid.NewGuid().ToString() };
        try
        {
            var result = await this._UsuarioAppService.GetAll();
            if (result.IsValid)
            {
                var MsgRetornoVazio = "";
                if (result.Data.Records.Count == 0) { MsgRetornoVazio = "OBS.: Nenhum dado localizado com os Parâmetros enviados."; }
                
                response.Message = "Consulta realizada com sucesso! " + MsgRetornoVazio;
                response.Data = result.Data.Records.ToList();
                    
                return Ok(response);
            }
            else
            {
                throw new Exception("Campos inválidos");
            }
        }
        catch (Exception ex)
        {
            
            var handlingError = new HandlingErrorsHelper<UsuarioListResponse, UsuarioListValidationResult>();
            response = handlingError.FillValidationError(response, null, "Erro ao consultar dados do Usuário", ex.Message);
            return BadRequest(response);
        }
            
    }

    [HttpGet("consulta-por-id/{idUsuario}")]
    [Authorize]
    public async Task<IActionResult> BuscaPorId(string idUsuario)
    {
        var response = new UsuarioSingleResponse() { DtStart = DateTime.Now, RequestToken = Guid.NewGuid().ToString() };

        try
        {
            var result = await this._UsuarioAppService.Get(idUsuario);
            var MsgRetornoVazio = "";

            if (result.Data == null)
                MsgRetornoVazio = "OBS.: Nenhum registro localizado.";
            else
            {
                if (!result.IsValid)
                {
                    throw new Exception("Campos inválidos");
                }
                else
                    response.Data = result.Data.ToSingleResponse();
            }

            response.Message = "Consulta realizada com sucesso! " + MsgRetornoVazio;
            response.ResultCode = (int)HttpStatusCode.OK;
            return Ok(response);

        }
        catch (Exception ex)
        {
            var handlingError = new HandlingErrorsHelper<UsuarioSingleResponse, UsuarioListValidationResult>();
            response = handlingError.FillValidationError(response, null, "Erro ao consultar dados do Usuário", ex.Message);
            return BadRequest(response);

        }

        
    }


    [HttpGet("consulta-paciente-por-id/{cns}")]
    public IActionResult BuscaPacientePorId(string cns)
    {
        var response = new PacienteSingleResponse() { DtStart = DateTime.Now, RequestToken = Guid.NewGuid().ToString() };

        try
        {
            var result =  this._PacienteAppService.GetPacientePorCNS(cns);
            var MsgRetornoVazio = "";

            if (result.Data == null)
                MsgRetornoVazio = "OBS.: Nenhum registro localizado.";
            else
            {
                if (!result.IsValid)
                {
                    throw new Exception("Campos inválidos");
                }
                else
                    response.Data = result.Data;
            }

            response.Message = "Consulta realizada com sucesso! " + MsgRetornoVazio;
            response.ResultCode = (int)HttpStatusCode.OK;
            return Ok(response);

        }
        catch (Exception ex)
        {
            var handlingError = new HandlingErrorsHelper<PacienteSingleResponse, PacienteListValidationResult>();
            response = handlingError.FillValidationError(response, null, "Erro ao consultar dados do Paciente", ex.Message);
            return BadRequest(response);

        }


    }
}
