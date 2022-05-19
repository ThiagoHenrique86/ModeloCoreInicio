using Prodesp.Application.CrossCutting.TO.Response.Medex.Paciente;
using Prodesp.Core.Backend.Application;
using Prodesp.Core.Backend.Domain.Models;

namespace Prodesp.Application.Validation.Medex;

public class PacienteValidationResult : AppValidationResult<PacienteResponseData> { }
public class PacienteListValidationResult : AppValidationResult<PageResult<PacienteResponseData>> { }

