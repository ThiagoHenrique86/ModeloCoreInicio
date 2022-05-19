

using Prodesp.Gsnet.Core.TO.Response;
using System.Runtime.Serialization;

namespace Prodesp.CrossCutting.TO.Response.GVE;
[Serializable]
[DataContract(Name = "GVEResponse", Namespace = "Prodesp.VaciVida.TO.Response")]
public class GVESingleResponse : SingleResponse<GVEResponseData> { }

[Serializable]
[DataContract(Name = "GVEListaResponse", Namespace = "Prodesp.VaciVida.TO.Response")]
public class GVEListResponse : ListResponse<GVEResponseData> { }
