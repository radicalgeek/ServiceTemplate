using System;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Topology;
using Serilog;
using Service.Messaging.Consume;

namespace Service.Messaging.Subscribe
{
    /// <summary>
    /// Automaticly subscribes to messages, by locating consumers for message types
    /// </summary>
    public class MessageSubscriber : IMessageSubscriber
    {
        private readonly IAdvancedBus _bus;
        private readonly IMessageConsumer _messageConsumer;
        private IDisposable _consumer;
        private readonly IExchange _exchange;
        private readonly IQueue _queue;

        public MessageSubscriber(IAdvancedBus bus,
            IMessageConsumer messageConsumer, 
            IExchange exchange,
            IQueue queue)
        {
            _messageConsumer = messageConsumer;
            _bus = bus;
            _exchange = exchange;
            _queue = queue;
        }

        /// <summary>
        /// Declares a new Exchange and Queue, and registers a consumer
        /// </summary>
        public void Start()
        {     
            //TODO: move routing key to config
            Log.Information("Event=\"Binding message queue to exchange\" Exchange=\"{0}\" Queue=\"{1}\" ", _exchange.Name, _queue.Name);
            _bus.Bind(_exchange, _queue, "A.*");

            Log.Information("Event=\"Subscribing to message queue\" Queue=\"{0}\" ", _queue.Name);
             _consumer =  _bus.Consume<object>(_queue, (message, info) =>
                Task.Factory.StartNew(() =>
                    _messageConsumer.Consume(message)
                    )
                );
        }

        public void Stop()
        {
            Log.Information("Event=\"Unsubscribing from message queue\" Queue=\"{0}\" ", _queue.Name);
            _consumer.Dispose();
        }
    }
}