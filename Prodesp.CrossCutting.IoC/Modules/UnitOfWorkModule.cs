using Ninject.Modules;
using Ninject.Web.Common;
using Prodesp.Infra.EF.UnitOfWorkCore;

namespace Prodesp.CrossCutting.IoC.Modules;
public class UnitOfWorkModule : NinjectModule
{
    public override void Load()
    {
        Bind<IUnitOfWork<Prodesp.Infra.EF.Contexto>>().To<UnitOfWork>().InRequestScope();
    }
}