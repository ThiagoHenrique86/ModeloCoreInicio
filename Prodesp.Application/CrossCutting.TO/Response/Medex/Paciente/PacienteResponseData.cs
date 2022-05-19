using System.Runtime.Serialization;

namespace Prodesp.Application.CrossCutting.TO.Response.Medex.Paciente;

[Serializable]
[DataContract(Name = "PacienteResponseData", Namespace = "Prodesp.Gsnet.RemedioCerto.Paciente.WebAPI.TO.Response")]
public class PacienteResponseData
{
    [DataMember]
    public string CodigoPaciente { get; set; }
    [DataMember]
    public string NomePaciente { get; set; }
    [DataMember]
    public string NomePacienteFonetico { get; set; }
    [DataMember]
    public string DataInclusao { get; set; }
    [DataMember]
    public string DataNascimento { get; set; }
    [DataMember]
    public string NomeMae { get; set; }
    [DataMember]
    public string CodigoSexo { get; set; }
    [DataMember]
    public decimal? CNS { get; set; }
    [DataMember]
    public string CPF { get; set; }
    [DataMember]
    public string RG { get; set; }
}