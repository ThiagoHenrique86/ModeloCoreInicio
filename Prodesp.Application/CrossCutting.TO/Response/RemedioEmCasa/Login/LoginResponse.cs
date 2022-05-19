
using Prodesp.Gsnet.Core.TO.Response;
using System.Runtime.Serialization;

namespace Prodesp.Application.CrossCutting.TO.Response.Login;

[Serializable]
[DataContract(Name = "LoginResponse", Namespace = "Prodesp.TO.Response")]
public class LoginSingleResponse : SingleResponse<LoginResponseData>
{
    [DataMember]
    public string MensagemUltimaTentativa { get; set; }
    [DataMember]
    public string Data { get; set; }


}

[Serializable]
[DataContract(Name = "LoginResponse", Namespace = "Prodesp.TO.Response")]
public class LoginUsuarioSingleResponse : SingleResponse<LoginResponseData>
{

}
public class LoginUsuarioPerfilSingleResponse : SingleResponse<LoginResponseData>
{

}

[Serializable]
[DataContract(Name = "LoginListaResponse", Namespace = "Prodesp.TO.Response")]
public class LoginListResponse : ListResponse<LoginResponseData> { }