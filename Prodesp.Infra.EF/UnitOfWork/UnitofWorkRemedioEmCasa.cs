using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Prodesp.Infra.EF.UnitOfWorkCore;

    public class UnitofWorkRemedioEmCasa : BaseUnitOfWork<RemedioEmCasaContexto>
    {
        public UnitofWorkRemedioEmCasa(IConfiguration configuration)
            : base(configuration.GetConnectionString("RemedioEmCasa"), Helpers.BancoHelper.TipoBanco.Oracle)
        {
            
        }
    }
