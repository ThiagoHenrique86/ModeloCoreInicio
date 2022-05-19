using Prodesp.Core.Backend.Domain.Interfaces;
using Prodesp.Domain.Shared.Entities;

namespace Prodesp.Domain.Services.Interfaces.Medex;

public interface IPacienteInteraction: IEntityInteractions<Paciente>
{
    Shared.Entities.Paciente? GetPacientePorCNS(string cns);
    Shared.Entities.Paciente? GetPacientePorId(string id);
}