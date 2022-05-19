using System.Runtime.Serialization;

namespace Prodesp.Application.CrossCutting.TO.Request.Login;

[Serializable]
[DataContract(Name = "LoginRequestData", Namespace = "Prodesp.TO.Request")]
public class LoginRequestData
{
    [DataMember]
    public string Login { get; set; }
    [DataMember]
    public string Senha { get; set; }
}