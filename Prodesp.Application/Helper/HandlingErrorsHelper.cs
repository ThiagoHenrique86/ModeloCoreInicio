using Prodesp.Core.Backend.Application;
using Prodesp.Core.Backend.Domain.Interfaces.Domain.Entities;
using Prodesp.Core.Backend.Domain.Validations;
using Prodesp.Gsnet.Core.TO.Response;

namespace Prodesp.Application.Helper;

public class HandlingErrorsHelper<TipoListResponse, TipoAppValidationResult> 
       where TipoListResponse        : BaseResponse 
       where TipoAppValidationResult : ValidationResult
{
    public TipoListResponse FillValidationError(TipoListResponse response, TipoAppValidationResult? result, string ErrorTitle, string? exceptionMessage) 
    {
        response.ValidationSummary = new Gsnet.Core.TO.Validation.ValidationData()
        {
            ResultCode = 0,
            Sucesso = false,
            Title = ErrorTitle,
            Erros = new List<Gsnet.Core.TO.Validation.FieldErrorData>()
        };

        if(!ReferenceEquals(result, null))
        {
            result.Errors.ToList().ForEach(err =>
            {
                response.ValidationSummary.Erros.Add(new Gsnet.Core.TO.Validation.FieldErrorData()
                {
                    ErrorMessage = err.Message
                });
            });
        }
        else
        {
            if (response.ValidationSummary == null)
            {
                response.ValidationSummary = new Gsnet.Core.TO.Validation.ValidationData()
                {
                    Data = exceptionMessage,
                    ResultCode = 0,
                    Sucesso = false,
                    Title = ErrorTitle
                };
            }
            else
            {
                if (response.ValidationSummary.Erros == null)
                    response.ValidationSummary.Erros = new List<Gsnet.Core.TO.Validation.FieldErrorData>();
            }

            response.ValidationSummary.Erros.Add(new Gsnet.Core.TO.Validation.FieldErrorData
            {
                ErrorMessage = exceptionMessage,
                FieldDesc = "?",
                FieldName = "?"
            });
        }

        return response;

    }

    /*
    public static void FillValidationError2(this ListResponse<T> response, GVEListValidationResult result, string ErrorTitle, string? exceptionMessage) 
    {
        response.ValidationSummary = new Gsnet.Core.TO.Validation.ValidationData()
        {
            ResultCode = 0,
            Sucesso = false,
            Title = ErrorTitle,
            Erros = new List<Gsnet.Core.TO.Validation.FieldErrorData>()
        };

        if (!ReferenceEquals(result, null))
        {
            result.Errors.ToList().ForEach(err =>
            {
                response.ValidationSummary.Erros.Add(new Gsnet.Core.TO.Validation.FieldErrorData()
                {
                    ErrorMessage = err.Message
                });
            });
        }
        else
        {
            if (response.ValidationSummary == null)
            {
                response.ValidationSummary = new Gsnet.Core.TO.Validation.ValidationData()
                {
                    Data = exceptionMessage,
                    ResultCode = 0,
                    Sucesso = false,
                    Title = ErrorTitle
                };
            }
            else
            {
                if (response.ValidationSummary.Erros == null)
                    response.ValidationSummary.Erros = new List<Gsnet.Core.TO.Validation.FieldErrorData>();
            }

            response.ValidationSummary.Erros.Add(new Gsnet.Core.TO.Validation.FieldErrorData
            {
                ErrorMessage = exceptionMessage,
                FieldDesc = "?",
                FieldName = "?"
            });
        }

    }*/
}

