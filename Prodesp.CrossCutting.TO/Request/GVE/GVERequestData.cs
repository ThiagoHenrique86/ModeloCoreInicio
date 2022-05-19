using System.Runtime.Serialization;

namespace Prodesp.CrossCutting.TO.Request.GVE;

[Serializable]
[DataContract(Name = "GVERequestData", Namespace = "Prodesp.VaciVida.TO.Request")]
public class GVERequestData
{

    [DataMember]
    public string IdGVE { get; set; } = "";
    [DataMember]
    public string DescricaoGVE { get; set; } = "";
}