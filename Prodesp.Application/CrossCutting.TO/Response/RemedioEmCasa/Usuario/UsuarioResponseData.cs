using System.Runtime.Serialization;

namespace Prodesp.Application.CrossCutting.TO.Response.Usuario;

[Serializable]
[DataContract(Name = "UsuarioResponseData", Namespace = "Prodesp.TO.Response")]
public class UsuarioResponseData
{
    [DataMember]
    public string IdUsuario { get; set; }
    [DataMember]
    public string NomeUsuario { get; set; }
    [DataMember]
    public int Ativo { get; set; }
    [DataMember]
    public DateTime DataInclusao { get; set; }
    [DataMember]
    public DateTime? DataAlteracao { get; set; }
    [DataMember]
    public string IdUsuarioInclusao { get; set; }
    [DataMember]
    public string IdUsuarioAlteracao { get; set; }
    
}


[Serializable]
[DataContract(Name = "UsuarioLoginResponseData", Namespace = "Prodesp.TO.Response")]
public class UsuarioLoginResponseData
{
    [DataMember]
    public string IdUsuario { get; set; }
    [DataMember]
    public string NomeUsuario { get; set; }
    
}

[Serializable]
[DataContract(Name = "UsuarioPerfisResponseData", Namespace = "Prodesp.TO.Response")]
public class UsuarioPerfisResponseData
{
    [DataMember]
    public string IdUsuario { get; set; }
    [DataMember]
    public string NomeUsuario { get; set; }

}


[Serializable]
[DataContract(Name = "RecuperacaoSenhaResponseData", Namespace = "Prodesp.TO.Response")]
public class RecuperacaoSenhaResponseData
{
    [DataMember]
    public string Destinatario { get; set; }
    [DataMember]
    public string Validade { get; set; }
}

[Serializable]
[DataContract(Name = "ValidacaoRecuperacaoResponseData", Namespace = "Prodesp.TO.Response")]
public class ValidacaoRecuperacaoResponseData
{
    [DataMember]
    public bool valido { get; set; }
}


[Serializable]
[DataContract(Name = "ConfirmacaoSenhaResponseData", Namespace = "Prodesp.TO.Response")]
public class ConfirmacaoSenhaResponseData
{
    [DataMember]
    public string DataSolicitacao { get; set; }
    [DataMember]
    public string DataConfirmacao { get; set; }
}



[Serializable]
[DataContract(Name = "AlterarSenhaUsuarioResponseData", Namespace = "Prodesp.TO.Response")]
public class AlterarSenhaUsuarioResponseData
{
    [DataMember]
    public bool Sucesso { get; set; }
}


[Serializable]
[DataContract(Name = "ResetarSenhaUsuarioResponseData", Namespace = "Prodesp.TO.Response")]
public class ResetarSenhaUsuarioResponseData
{
    [DataMember]
    public string Login { get; set; }
    [DataMember]
    public string SenhaProvisoria { get; set; }
    [DataMember]
    public string TextoRetorno { get; set; }
}
