using Autofac;
using Service.Logging;

namespace Service.Host
{
    public static class DependancyContainer
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Logger>().As<ILogger>();
            builder.RegisterType<MicroServiceLogic>().As<IHostableMicroService>();
            return builder.Build();
        }
    }
}