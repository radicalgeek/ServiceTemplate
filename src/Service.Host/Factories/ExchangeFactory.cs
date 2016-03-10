using EasyNetQ;
using EasyNetQ.Topology;
using Microsoft.Extensions.Configuration;

namespace Service.Host.Factories
{
    public static class ExchangeFactory
    {
        public static IExchange CreatExchange(IAdvancedBus bus, IConfigurationRoot environmentConfiguration)
        {      
            var exchange = bus.ExchangeDeclare(environmentConfiguration.Get("ExchangeName"), ExchangeType.Topic);
            return exchange;
        }
    }

}