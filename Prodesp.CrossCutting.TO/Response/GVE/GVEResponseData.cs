
using System.Runtime.Serialization;

namespace Prodesp.CrossCutting.TO.Response.GVE;
[Serializable]
[DataContract(Name = "GVEResponseData", Namespace = "Prodesp.VaciVida.TO.Response")]
public class GVEResponseData
{
    [DataMember]
    public string IdGVE { get; set; } = string.Empty;   
    [DataMember]
    public string DescricaoGVE { get; set; } = string.Empty;
}