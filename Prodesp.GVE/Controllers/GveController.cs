using Microsoft.AspNetCore.Mvc;
using Prodesp.Application.AppServices;
using Prodesp.Application.Helper;
using Prodesp.CrossCutting.TO.Response.GVE;
using Prodesp.Gsnet.Core.TO.Validation;
using System.Net;

namespace Prodesp.GVE.Controllers;

    [ApiController]
    [Route("[controller]")]
    public class GveController : ControllerBase
    {
        public IGVEAppService _service { get; set; }
        
        public GveController(IGVEAppService service)
                
        {
            this._service = service;
        }

        [HttpGet("consulta-todos-gve")]
        public GVEListResponse ConsultaTodosGVE()
        {
            var response = new GVEListResponse() { DtStart = DateTime.Now, RequestToken = Guid.NewGuid().ToString() };
           
            var erros_lista = new List<FieldErrorData>();
            try
            {
                
                var result = this._service.GetAll();
                if (result.IsValid)
                {
                    var MsgRetornoVazio = "";
                    if (result.Data.Records.Count == 0) { MsgRetornoVazio = "OBS.: Nenhum dado localizado com os Parâmetros enviados."; }

                    response.Message = "Consulta realizada com sucesso! " + MsgRetornoVazio;
                    response.Data = result.ToListResponse().Data;
                    response.ResultCode = 200;
                }
                else
                {
                    response.ValidationSummary = new Gsnet.Core.TO.Validation.ValidationData()
                    {
                        ResultCode = 0,
                        Sucesso = false,
                        Title = "Erro ao consultar dados do Gve",
                        Erros = new List<Gsnet.Core.TO.Validation.FieldErrorData>()
                    };

                    result.Errors.ToList().ForEach(err =>
                    {
                        response.ValidationSummary.Erros.Add(new Gsnet.Core.TO.Validation.FieldErrorData()
                        {
                            ErrorMessage = err.Message
                        });
                    });
                }

                //}

            }
            catch (Exception ex)
            {
                if (response.ValidationSummary == null)
                {
                    response.ValidationSummary = new Gsnet.Core.TO.Validation.ValidationData()
                    {
                        Data = ex.Message,
                        ResultCode = 0,
                        Sucesso = false,
                        Title = "Erro ao consultar dados do Gve"
                    };
                }
                else
                {
                    if (response.ValidationSummary.Erros == null)
                        response.ValidationSummary.Erros = new List<Gsnet.Core.TO.Validation.FieldErrorData>();
                }

                response.ValidationSummary.Erros.Add(new Gsnet.Core.TO.Validation.FieldErrorData
                {
                    ErrorMessage = ex.Message,
                    FieldDesc = "?",
                    FieldName = "?"
                });

            }
            return response;
        }


    [HttpGet("consulta-por-idGVE/{idGVE}")]
    public GVESingleResponse BuscaPorId(string idGVE)
    {
        var response = new GVESingleResponse() { DtStart = DateTime.Now, RequestToken = Guid.NewGuid().ToString() };

        var erros_lista = new List<FieldErrorData>();
        try
        {
            
            var result = this._service.Get(idGVE);
            var MsgRetornoVazio = "";

            if (result.Data == null)
                MsgRetornoVazio = "OBS.: Nenhum registro localizado.";
            else
            {
                if (!result.IsValid)
                {
                    response.ValidationSummary = new ValidationData { Erros = new List<FieldErrorData>() };
                    result.Errors.ToList().ForEach(err => response.ValidationSummary.Erros.Add(new FieldErrorData { ErrorMessage = err.Message }));
                }
                else
                    response.Data = result.Data.ToSingleResponse();
            }

            response.Message = "Consulta realizada com sucesso! " + MsgRetornoVazio;
            response.ResultCode = (int)HttpStatusCode.OK;


        }
        catch (Exception ex)
        {
            if (response.ValidationSummary == null)
            {
                response.ValidationSummary = new Gsnet.Core.TO.Validation.ValidationData()
                {
                    Data = ex.Message,
                    ResultCode = 0,
                    Sucesso = false,
                    Title = "Erro ao consultar dados do Gve"
                };
            }
            else
            {
                if (response.ValidationSummary.Erros == null)
                    response.ValidationSummary.Erros = new List<Gsnet.Core.TO.Validation.FieldErrorData>();
            }

            response.ValidationSummary.Erros.Add(new Gsnet.Core.TO.Validation.FieldErrorData
            {
                ErrorMessage = ex.Message,
                FieldDesc = "?",
                FieldName = "?"
            });

        }
        return response;
    }
}
