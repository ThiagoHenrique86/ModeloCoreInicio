using System.Runtime.Serialization;

namespace Prodesp.Application.CrossCutting.TO.Request.Usuario;

[Serializable]
[DataContract(Name = "UsuarioRequestData", Namespace = "Prodesp.TO.Request")]
public class UsuarioRequestData
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
[DataContract(Name = "RecuperacaoSenhaRequestData", Namespace = "Prodesp.TO.Request")]
public class RecuperacaoSenhaRequestData
{
     [DataMember]
    public string Login { get; set; }
}

[Serializable]
[DataContract(Name = "ConfirmacaoSenhaRequestData", Namespace = "Prodesp.TO.Request")]
public class ConfirmacaoSenhaRequestData
{
    [DataMember]
    public string Login { get; set; }
    [DataMember]
    public string CodigoRecuperacao { get; set; }
    [DataMember]
    public string Senha { get; set; }
    [DataMember]
    public string ConfirmacaoSenha { get; set; }
}

[Serializable]
[DataContract(Name = "AlterarDadosUsuarioRequestData", Namespace = "Prodesp.TO.Request")]
public class AlterarDadosUsuarioRequestData
{
    [DataMember]
    public string Nome { get; set; }
}


[Serializable]
[DataContract(Name = "AlterarSenhaRequestData", Namespace = "Prodesp.TO.Request")]
public class AlterarSenhaUsuarioRequestData
{
    [DataMember]
    public string SenhaAtual { get; set; }
    [DataMember]
    public string NovaSenha { get; set; }
    [DataMember]
    public string ConfirmacaoSenha { get; set; }
}
