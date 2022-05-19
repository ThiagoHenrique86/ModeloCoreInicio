
using Prodesp.Gsnet.Core.TO.Request;
using System.Runtime.Serialization;

namespace Prodesp.Application.CrossCutting.TO.Request.Usuario;

[Serializable]
[DataContract(Name = "UsuarioRequest", Namespace = "Prodesp.TO.Request")]
public class UsuarioRequest : SingleRequest<UsuarioRequestData>
{

}

[Serializable]
[DataContract(Name = "UsuarioSingleSearchRequest", Namespace = "Prodesp.CrossCuting.TO.Request.Usuario")]
public class UsuarioSingleSearchRequest : CustomSearchRequest
{
    [DataMember]
    public string IdUsuario { get; set; }
}


[Serializable]
[DataContract(Name = "RecuperacaoSenhaRequest", Namespace = "Prodesp.CrossCuting.TO.Request.Usuario")]
public class RecuperacaoSenhaRequest : SingleRequest<RecuperacaoSenhaRequestData>
{

}

[Serializable]
[DataContract(Name = "ConfirmacaoSenhaRequest", Namespace = "Prodesp.CrossCuting.TO.Request.Usuario")]
public class ConfirmacaoSenhaRequest : SingleRequest<ConfirmacaoSenhaRequestData>
{

}

[Serializable]
[DataContract(Name = "AlterarDadosUsuarioRequest", Namespace = "Prodesp.CrossCuting.TO.Request.Usuario")]
public class AlterarDadosUsuarioRequest : SingleRequest<AlterarDadosUsuarioRequestData>
{

}

[Serializable]
[DataContract(Name = "AlterarSenhaUsuarioRequest", Namespace = "Prodesp.CrossCuting.TO.Request.Usuario")]
public class AlterarSenhaUsuarioRequest : SingleRequest<AlterarSenhaUsuarioRequestData>
{

}