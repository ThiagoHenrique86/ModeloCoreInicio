using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Prodesp.Infra.EF.UnitOfWorkCore;

    public class UnitofWorkGsnet: BaseUnitOfWork<GsnetContexto>
    {
        public UnitofWorkGsnet(IConfiguration configuration)
            : base(configuration.GetConnectionString("GSNET"), Helpers.BancoHelper.TipoBanco.Oracle)
        {
            
        }
    }
