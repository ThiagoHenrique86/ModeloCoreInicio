
using Prodesp.Core.Backend.Domain.Interfaces.Domain.Services;
using Prodesp.Core.Backend.Domain.Services;
using Prodesp.Domain.Repositories.Interfaces.Medex;
using Prodesp.Domain.Services.Interfaces.Medex;
using System.Data.Entity.Core.Objects;

namespace Prodesp.Domain.Services.Implementations.Medex;

public class PacienteService :
        Service<Shared.Entities.Paciente, IPacienteRepository>, IPacienteService
{
    public PacienteService(IPacienteRepository _PacienteRepository) : base(_PacienteRepository) { }

    public Shared.Entities.Paciente? GetPacientePorCNS(string cns)
    {
        if (base._repository == null)
            return null;

        decimal numCNS;
        if (Decimal.TryParse(cns, out numCNS))
        {
            return base._repository.Find(x => x.CNS == numCNS).FirstOrDefault();
        }
        else
            throw new ApplicationException("CNS inválido");
    }

    public Shared.Entities.Paciente? GetPacientePorId(string id)
    {
        if (base._repository == null)
            return null;



        //return base._repository.Find(x => x.CodigoPaciente == id).FirstOrDefault();
        return base._repository.Find(x => x.CodigoPaciente != id).FirstOrDefault();
    }
}

public interface IPacienteService : IService<Shared.Entities.Paciente>, IPacienteInteraction
{
}