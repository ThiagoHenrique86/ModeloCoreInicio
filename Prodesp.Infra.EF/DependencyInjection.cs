using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prodesp.Application.AppServices;
using Prodesp.Application.AppServices.Medex;
using Prodesp.Application.Helper;
using Prodesp.Domain.Repositories.Interfaces;
using Prodesp.Domain.Repositories.Interfaces.Medex;
using Prodesp.Domain.Services.Implementations;
using Prodesp.Domain.Services.Implementations.Medex;
using Prodesp.Domain.Services.Interfaces;
using Prodesp.Infra.EF.AutoMapper;
using Prodesp.Infra.EF.Repositories;
using Prodesp.Infra.EF.Repositories.Medex;
using Prodesp.Infra.EF.UnitOfWorkCore;
using static Prodesp.Infra.EF.UnitOfWorkCore.IUnitOfWork;

namespace Prodesp.Infra.EF;

public static class DependencyInjection
{
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        /*services.AddDbContext<Contexto>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("SQLSERVER"), options =>
                    {
                        options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    }));
        */
        //services.AddScoped<Contexto>(provider => provider.GetRequiredService<Contexto>());
        


        
        services.AddScoped<IUsuarioAppService, UsuarioAppService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        services.AddScoped<ILoginAppService, LoginAppService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<ILoginRepository, LoginRepository>();

        services.AddScoped<ISessaoAppService, SessaoAppService>();
        services.AddScoped<ISessaoService, SessaoService>();
        services.AddScoped<ISessaoRepository, SessaoRepository>();

        services.AddScoped<IPacienteService, PacienteService>();
        services.AddScoped<IPacienteRepository, PacienteRepository>();
        services.AddScoped<IPacienteAppService, PacienteAppService>();

        services.AddScoped<ITokenHelperAppService, TokenHelperAppService>();
        services.AddScoped<IUnitOfWork<RemedioEmCasaContexto>, UnitofWorkRemedioEmCasa>();
        services.AddScoped<IUnitOfWork<MedexContexto>, UnitofWorkMedex>();
        services.AddScoped<IUnitOfWork<GsnetContexto>, UnitofWorkGsnet>();

        services.AddCors(options =>
        {
            options.AddPolicy(
                name: "AllowOrigin",
                builder => {
                    builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                });
        });

        var sp = services.BuildServiceProvider();
        SessaoAppServiceHelper.SessaoAppServiceHelperConfigure(sp.GetService<IConfiguration>());
        services.AddAutoMapper(typeof(DomainToResponseMappingProfile), typeof(ResponseToDomainMappingProfile));

        return services;
    }

}