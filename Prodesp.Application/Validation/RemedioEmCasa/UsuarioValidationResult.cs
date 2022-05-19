using Prodesp.Application.CrossCutting.TO.Response.Usuario;
using Prodesp.Core.Backend.Application;
using Prodesp.Core.Backend.Domain.Models;

namespace Prodesp.Application.Validation;

public class UsuarioValidationResult : AppValidationResult<Domain.Shared.Entities.Usuario> { }
public class UsuarioListValidationResult : AppValidationResult<PageResult<Domain.Shared.Entities.Usuario>> { }

public class UsuarioDataListValidationResult : AppValidationResult<PageResult<UsuarioResponseData>> { }
//public class RecuperacaoSenhaResult : AppValidationResult<Domain.Shared.Entities.UsuarioRecupera
public class AlterarSenhaUsuarioResult : AppValidationResult<AlterarSenhaUsuarioResponseData> { }

public class ResetarSenhaUsuarioResult : AppValidationResult<ResetarSenhaUsuarioResponseData> { }