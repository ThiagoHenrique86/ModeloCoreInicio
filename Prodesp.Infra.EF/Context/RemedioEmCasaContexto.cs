using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Prodesp.Domain.Shared.Entities;
using Prodesp.Infra.EF.Configurations.RemedioEmCasa;

namespace Prodesp.Infra.EF;

public class RemedioEmCasaContexto : DbContext
{

    public RemedioEmCasaContexto(DbContextOptions<RemedioEmCasaContexto> options) : base(options)
    {
        //this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

    }

    public static readonly ILoggerFactory _loggerFactory
                    = LoggerFactory.Create(builder => builder.AddDebug().AddFilter((category, level) => level == LogLevel.Information && !category.EndsWith("Connection")));


  
    public virtual DbSet<Login> Login { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<Sessao> Sessao { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(UsuarioConfiguration).Assembly, m => m?.Namespace == "Prodesp.Infra.EF.Configurations.RemedioEmCasa"); 
        
        builder.Entity<Login>().HasOne(q => q.Usuario).WithOne().IsRequired();

        base.OnModelCreating(builder);
    }

}