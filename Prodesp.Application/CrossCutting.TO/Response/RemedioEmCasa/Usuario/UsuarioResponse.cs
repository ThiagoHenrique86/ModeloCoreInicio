
using Prodesp.Gsnet.Core.TO.Response;
using System.Runtime.Serialization;

namespace Prodesp.Application.CrossCutting.TO.Response.Usuario;
[Serializable]
[DataContract(Name = "UsuarioResponse", Namespace = "Prodesp.TO.Response")]
public class UsuarioSingleResponse : SingleResponse<UsuarioResponseData> { }

[Serializable]
[DataContract(Name = "UsuarioLoginSingleResponse", Namespace = "Prodesp.TO.Response")]
public class UsuarioLoginSingleResponse : SingleResponse<UsuarioLoginResponseData> { }

[Serializable]
[DataContract(Name = "UsuarioListaResponse", Namespace = "Prodesp.TO.Response")]
public class UsuarioListResponse : ListResponse<UsuarioResponseData> { }

[Serializable]
[DataContract(Name = "UsuarioRecuperacaoSenhaSingleResponse", Namespace = "Prodesp.TO.Response")]
public class UsuarioRecuperacaoSenhaSingleResponse : SingleResponse<RecuperacaoSenhaResponseData> { }

[Serializable]
[DataContract(Name = "UsuarioValicacaoRecuperacaoSingleResponse", Namespace = "Prodesp.TO.Response")]
public class UsuarioValicacaoRecuperacaoSingleResponse : SingleResponse<ValidacaoRecuperacaoResponseData> { }

[Serializable]
[DataContract(Name = "UsuarioConfirmacaoSenhaSingleResponse", Namespace = "Prodesp.TO.Response")]
public class UsuarioConfirmacaoSenhaSingleResponse : SingleResponse<ConfirmacaoSenhaResponseData> { }

[Serializable]
[DataContract(Name = "AlterarSenhaSingleResponse", Namespace = "Prodesp.TO.Response")]
public class AlterarSenhaUsuarioSingleResponse : SingleResponse<AlterarSenhaUsuarioResponseData> { }


[Serializable]
[DataContract(Name = "AlterarSenhaSingleResponse", Namespace = "Prodesp.TO.Response")]
public class ResetarSenhaUsuarioSingleResponse : SingleResponse<ResetarSenhaUsuarioResponseData> { }
