using EasyNetQ;
using EasyNetQ.Topology;
using Microsoft.Extensions.Configuration;

namespace Service.Host.Factories
{
    public static class QueueFactory
    {
        public static IQueue CreatQueue(IAdvancedBus bus, IConfigurationRoot environmentConfiguration)
        {
            var queue = bus.QueueDeclare(environmentConfiguration.Get("QueueName"));
            return queue;
        }
    }
}