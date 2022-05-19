using Prodesp.Application.CrossCutting.TO.Response.Usuario;
using System.Runtime.Serialization;

namespace Prodesp.Application.CrossCutting.TO.Response.Login;

[Serializable]
[DataContract(Name = "LoginResponseData", Namespace = "Prodesp.TO.Response")]
public class LoginResponseData
{
    [DataMember]
    public UsuarioLoginResponseData Usuario { get; set; }
   
   [DataMember]
    public string CodigoLogin { get; set; }
    [DataMember]
    public string NumeroTentativaLogin { get; set; }
    [DataMember]
    public int FlagBloqueado { get; set; }
    [DataMember]
    public int FlagAtivo { get; set; }
    [DataMember]
    public bool PrimeiroAcesso { get; set; }
   
    
    

    //[DataMember]
    //public SessaoResponseData Sessao { get; set; }
}