
using Prodesp.Domain.Repositories.Interfaces.Medex;
using Prodesp.Domain.Shared.Entities;
using static Prodesp.Infra.EF.UnitOfWorkCore.IUnitOfWork;

namespace Prodesp.Infra.EF.Repositories.Medex;

public class PacienteRepository : Repository<Paciente, MedexContexto>, IPacienteRepository
{
    public PacienteRepository(IUnitOfWork<MedexContexto> uow) :
        base(uow)
    {

    }
}