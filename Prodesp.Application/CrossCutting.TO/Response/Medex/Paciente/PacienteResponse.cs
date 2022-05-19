using Prodesp.Gsnet.Core.TO.Response;
using System.Runtime.Serialization;

namespace Prodesp.Application.CrossCutting.TO.Response.Medex.Paciente;
[Serializable]
[DataContract(Name = "PacienteResponse", Namespace = "Prodesp.Gsnet.RemedioCerto.Paciente.WebAPI.TO.Response")]
public class PacienteSingleResponse : SingleResponse<PacienteResponseData> { }

[Serializable]
[DataContract(Name = "PacienteListaResponse", Namespace = "Prodesp.Gsnet.RemedioCerto.Paciente.WebAPI.TO.Response")]
public class PacienteListResponse : ListResponse<PacienteResponseData> { }


