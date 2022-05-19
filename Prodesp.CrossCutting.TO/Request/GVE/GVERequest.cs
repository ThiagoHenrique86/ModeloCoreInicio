
using Prodesp.Gsnet.Core.TO.Request;
using System.Runtime.Serialization;

namespace Prodesp.CrossCutting.TO.Request.GVE;

[Serializable]
[DataContract(Name = "GVERequest", Namespace = "Prodesp.VaciVida.TO.Request")]
public class GVERequest : SingleRequest<GVERequestData>
{

}

[Serializable]
[DataContract(Name = "GVESingleSearchRequest", Namespace = "Prodesp.VaciVida.CrossCuting.TO.Request.GVE")]
public class GVESingleSearchRequest : CustomSearchRequest
{
    [DataMember]
    public int IdGVE { get; set; }
}