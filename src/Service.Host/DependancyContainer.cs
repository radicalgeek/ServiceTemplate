using Autofac;
using EasyNetQ;
using Microsoft.Extensions.PlatformAbstractions;
using Service.Host.Factories;
using Microsoft.Extensions.Configuration;
using EasyNetQ.Topology;
using Service.Logic;

namespace Service.Host
{
    public static class DependancyContainer
    {
        public static Autofac.IContainer Configure(IConfigurationRoot environmentConfiguration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance<IConfigurationRoot>(environmentConfiguration);
            builder.RegisterInstance<IRuntimeEnvironment>(PlatformServices.Default.Runtime);
            builder.RegisterInstance<IApplicationEnvironment>(PlatformServices.Default.Application);
            builder.RegisterType<MicroServiceLogic>().As<IHostableMicroService>();
            builder.RegisterInstance<IAdvancedBus>(BusFactory.CreateMessageBus(environmentConfiguration));
            var container =   builder.Build();
            builder.RegisterInstance<IExchange>(ExchangeFactory.CreatExchange(container.Resolve<IAdvancedBus>(),environmentConfiguration));
            builder.RegisterInstance<IQueue>(QueueFactory.CreatQueue(container.Resolve<IAdvancedBus>(),environmentConfiguration));
            builder.Update(container);
            return container;
        }
    }
}