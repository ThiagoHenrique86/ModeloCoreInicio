using Microsoft.EntityFrameworkCore;
using Prodesp.Domain.Repositories.Interfaces;
using Prodesp.Domain.Shared.Entities;
using static Prodesp.Infra.EF.UnitOfWorkCore.IUnitOfWork;

namespace Prodesp.Infra.EF.Repositories;

public class SessaoRepository : Repository<Domain.Shared.Entities.Sessao, RemedioEmCasaContexto>, ISessaoRepository
{
    public SessaoRepository(IUnitOfWork<RemedioEmCasaContexto> uow) :
        base(uow)
    {

    }

    public async Task<Sessao?> GetSessaoByToken(string nome)
    {
        var sessao = await UnityOfWork.Contexto.Sessao.Where(l => l.CodigoAccessToken == nome)
                                                 .Include(u => u.Login)
                                                    .ThenInclude(us => us.Usuario).AsNoTracking().FirstOrDefaultAsync();

        return sessao;
    }
}
