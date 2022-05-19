using Prodesp.Core.Backend.Application;
using Prodesp.Core.Backend.Domain.Models;

namespace Prodesp.Application.Validation;

public class LoginValidationResult : AppValidationResult<Domain.Shared.Entities.Login> { }
public class LoginListValidationResult : AppValidationResult<PageResult<Domain.Shared.Entities.Login>> { }