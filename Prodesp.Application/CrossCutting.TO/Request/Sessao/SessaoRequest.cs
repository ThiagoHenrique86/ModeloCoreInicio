using Prodesp.Gsnet.Core.TO.Request;
using System.Runtime.Serialization;

namespace Prodesp.Application.CrossCutting.TO.Request.Sessao;

[Serializable]
[DataContract(Name = "SessaoRequest", Namespace = "Prodesp.VaciVida.TO.Request")]
public class SessaoRequest : SingleRequest<SessaoRequestData>
{

}

[Serializable]
[DataContract(Name = "SessaoSingleSearchRequest", Namespace = "Prodesp.VaciVida.CrossCuting.TO.Request.Sessao")]
public class SessaoSingleSearchRequest : CustomSearchRequest
{
    [DataMember]
    public int IdSessao { get; set; }
}