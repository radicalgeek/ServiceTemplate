using EasyNetQ;
using Microsoft.Extensions.Configuration;

namespace Service.Host.Factories
{
    public static class BusFactory
    {
        public static IAdvancedBus CreateMessageBus(IConfigurationRoot environmentConfiguration)
        {
            var connectionString = environmentConfiguration.Get("QueueConnection");
            return RabbitHutch.CreateBus(connectionString).Advanced;
        }
    }
}