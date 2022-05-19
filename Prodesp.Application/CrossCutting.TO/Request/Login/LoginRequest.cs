using Prodesp.Gsnet.Core.TO.Request;
using System.Runtime.Serialization;

namespace Prodesp.Application.CrossCutting.TO.Request.Login;

[Serializable]
[DataContract(Name = "LoginRequest", Namespace = "Prodesp.TO.Request")]
public class LoginRequest : SingleRequest<LoginRequestData>
{

}

[Serializable]
[DataContract(Name = "LoginSingleSearchRequest", Namespace = "Prodesp.CrossCuting.TO.Request.Login")]
public class LoginSingleSearchRequest : CustomSearchRequest
{
    [DataMember]
    public int IdLogin { get; set; }
}
