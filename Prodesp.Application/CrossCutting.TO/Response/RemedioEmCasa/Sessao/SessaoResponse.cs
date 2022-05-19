using Prodesp.Gsnet.Core.TO.Response;
using System.Runtime.Serialization;

namespace Prodesp.Application.CrossCutting.TO.Response.Sessao;

[Serializable]
[DataContract(Name = "SessaoResponse", Namespace = "Prodesp.VaciVida.TO.Response")]
public class SessaoSingleResponse : SingleResponse<SessaoResponseData> { }

[Serializable]
[DataContract(Name = "SessaoListaResponse", Namespace = "Prodesp.VaciVida.TO.Response")]
public class SessaoListResponse : ListResponse<SessaoResponseData> { }