using Ninject.Modules;
using Prodesp.Domain.Repositories.Interfaces;
using Prodesp.VaciVida.Infra.EF.Implementations;

namespace Prodesp.CrossCutting.IoC.Modules;

public class ApiRepositoryModule : NinjectModule
{
    public override void Load()
    {
        Bind<IGVERepository>().To<GVERepository>();
    }
}