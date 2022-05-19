
using System.Runtime.Serialization;

namespace Prodesp.Application.CrossCutting.TO.Request.Sessao;

[Serializable]
[DataContract(Name = "SessaoRequestData", Namespace = "Prodesp.VaciVida.TO.Request")]
public class SessaoRequestData
{
    [DataMember]
    public string IdAccessToken { get; set; }
    [DataMember]
    public string CodigoAccessToken { get; set; }
    [DataMember]
    public int QtdDuracaoAccessToken { get; set; }
    [DataMember]
    public DateTime DataValidadeAccessToken { get; set; }
    [DataMember]
    public string IdUsuario { get; set; }
   
    [DataMember]
    public string IdLogin { get; set; }
    [DataMember]
    public string NumeroIp { get; set; }
    [DataMember]
    public string NumeroHost { get; set; }
    [DataMember]
    public DateTime DataInclusao { get; set; }
    [DataMember]
    public DateTime DataAlteracao { get; set; }
    [DataMember]
    public string FlagRefreshAccessToken { get; set; }
    
}