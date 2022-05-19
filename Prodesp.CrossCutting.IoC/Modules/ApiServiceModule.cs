
using Ninject.Modules;
using Prodesp.Domain.Services.Implementations;
using Prodesp.Domain.Services.Interfaces;

namespace Prodesp.CrossCutting.IoC.Modules;

public class ApiServiceModule : NinjectModule
{
    public override void Load()
    {
        Bind<IGVEService>().To<GVEService>();
    }

}