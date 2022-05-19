using Ninject.Modules;
using Prodesp.CrossCutting.IoC.Modules;

namespace Prodesp.CrossCutting.IoC;

public class WebApiIoC : Core.Backend.CrossCutting.IoC.IoC
{
    public WebApiIoC() : base()
    {

    }

    public override INinjectModule[] Modules()
    {
        var list = base.Modules().ToList();

        list.Add(new ApiApplicationModule());
        list.Add(new ApiRepositoryModule());
        list.Add(new ApiServiceModule());
        list.Add(new UnitOfWorkModule());

        return list.ToArray();
    }
}
