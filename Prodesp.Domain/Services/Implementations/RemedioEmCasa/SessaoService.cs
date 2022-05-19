using Prodesp.Core.Backend.Domain.Models;
using Prodesp.Core.Backend.Domain.Services;
using Prodesp.Domain.Repositories.Interfaces;
using Prodesp.Domain.Services.Interfaces;
using Prodesp.Domain.Shared.Entities;

namespace Prodesp.Domain.Services.Implementations;

    public class SessaoService :
         Service<Shared.Entities.Sessao, ISessaoRepository>, ISessaoService
    {

        private readonly ISessaoRepository _SessaoRepository;

        public SessaoService(ISessaoRepository _SessaoRepository) : base(_SessaoRepository) { }

        public async Task<Sessao?> FindByAccessToken(string cdToken)
        {
            if (base._repository == null)
                return null;
            var sessao = await base._repository.FindAsync(x => x.CodigoAccessToken == cdToken);
            return sessao.FirstOrDefault();
        }

        public async Task<Sessao?> GetSessao(string idSessao)
        {
            if (base._repository == null)
                return null;
            var sessao = await base._repository.FindAsync(x => x.IdAccessToken == idSessao);
            return sessao.FirstOrDefault();
        }

        public async Task<PageResult<Sessao>?> GetAll()
        {
            if (base._repository == null)
                return null;

            var filter = await base._repository.SelectAllAsync();
            var hp = new Gsnet.Framework.HelperPage(1, int.MaxValue, filter.Count());
            return new PageResult<Shared.Entities.Sessao>
            {
                Records = filter.ToList(),
                Page = hp
            };
        }
    }

