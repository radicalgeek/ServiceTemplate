using Autofac;
using Microsoft.Extensions.PlatformAbstractions;

namespace Service.Host
{
    public static class DependancyContainer
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance<IRuntimeEnvironment>(PlatformServices.Default.Runtime);
            builder.RegisterInstance<IApplicationEnvironment>(PlatformServices.Default.Application);
            builder.RegisterType<MicroServiceLogic>().As<IHostableMicroService>();
            return builder.Build();
        }
    }
}