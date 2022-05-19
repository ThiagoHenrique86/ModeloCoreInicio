using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Prodesp.Domain.Shared.Entities;
using Prodesp.Infra.EF.Configurations.RemedioEmCasa;

namespace Prodesp.Infra.EF;

public class MedexContexto : DbContext
{

    public MedexContexto(DbContextOptions<MedexContexto> options) : base(options)
    {
        //this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

    }

    public static readonly ILoggerFactory _loggerFactory
                    = LoggerFactory.Create(builder => builder.AddDebug().AddFilter((category, level) => level == LogLevel.Information && !category.EndsWith("Connection")));


  

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(UsuarioConfiguration).Assembly, m => m?.Namespace == "Prodesp.Infra.EF.Configurations.Medex");


        base.OnModelCreating(builder);
    }

}