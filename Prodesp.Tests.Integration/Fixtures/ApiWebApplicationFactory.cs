
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Prodesp.Infra.EF;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Prodesp.GVE;

namespace Prodesp.Tests.Integration;
internal class FakeUserFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        throw new System.NotImplementedException();
    }

    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, "123"),
            new Claim(ClaimTypes.Name, "Test user"),
            new Claim(ClaimTypes.Email, "test@example.com"),
            new Claim(ClaimTypes.Role, "Admin")
        }));
    }
}

public class ApiWebApplicationFactory : WebApplicationFactory<Program>
{
    public IConfiguration Configuration { get; private set; }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        /*builder.ConfigureAppConfiguration(config => {
            Configuration = new ConfigurationBuilder()
                    .AddJsonFile("integrationsettings.json")
                    .Build();

            
            config.AddConfiguration(Configuration);

            builder.ConfigureServices(services => {
                services.AddInfrastructure(Configuration);
                services.AddEndpointsApiExplorer();

                /*services.AddMvc(options =>
                {
                    options.Filters.Add(new AllowAnonymousFilter());
                    options.Filters.Add(new FakeUserFilter());
                });

            });

        }); 
        */

        builder.ConfigureServices(services =>
        {
            Configuration = new ConfigurationBuilder()
                   .AddJsonFile("integrationsettings.json")
                   .Build();

            services.AddInfrastructure(Configuration);
            services.AddEndpointsApiExplorer();
            services.AddControllers();
        });


        }
}