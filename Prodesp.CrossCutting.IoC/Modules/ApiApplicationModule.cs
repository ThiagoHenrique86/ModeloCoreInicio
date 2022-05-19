using Ninject.Modules;
using Prodesp.Application.AppServices;

namespace Prodesp.CrossCutting.IoC.Modules;

    public class ApiApplicationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGVEAppService>().To<GVEAppService>();
        }

   }
