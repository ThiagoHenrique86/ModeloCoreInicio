using System;
using System.Runtime.Serialization;

namespace Prodesp.Application.CrossCutting.TO.Response.Sessao;

[Serializable]
[DataContract(Name = "SessaoResponseData", Namespace = "Prodesp.VaciVida.TO.Response")]
public class SessaoResponseData
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
    public string NomeHost { get; set; }
    [DataMember]
    public DateTime DataInclusao { get; set; }
    [DataMember]
    public DateTime DataAlteracao { get; set; }
    [DataMember]
    public string FlagRefreshAccessToken { get; set; }

}

