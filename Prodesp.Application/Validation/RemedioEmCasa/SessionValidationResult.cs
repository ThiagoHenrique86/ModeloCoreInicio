using Prodesp.Core.Backend.Application;
using Prodesp.Core.Backend.Domain.Models;

namespace Prodesp.Application.Validation;
public class SessaoValidationResult : AppValidationResult<Domain.Shared.Entities.Sessao> { }
public class SessaoListValidationResult : AppValidationResult<PageResult<Domain.Shared.Entities.Sessao>> { }