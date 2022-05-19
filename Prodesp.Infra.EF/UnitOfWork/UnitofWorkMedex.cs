using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Prodesp.Infra.EF.UnitOfWorkCore;

    public class UnitofWorkMedex: BaseUnitOfWork<MedexContexto>
    {
        public UnitofWorkMedex(IConfiguration configuration)
            : base(configuration.GetConnectionString("Medex.DBASES"), Helpers.BancoHelper.TipoBanco.Oracle)
        {
            
        }
    }
