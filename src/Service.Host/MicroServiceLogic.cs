using System;
using EasyNetQ;
using EasyNetQ.Topology;
using Microsoft.Extensions.PlatformAbstractions;
using Serilog;

namespace Service.Host
{
    public class MicroServiceLogic : IHostableMicroService
    {
        private IRuntimeEnvironment _env;
        private IApplicationEnvironment _app;
        private readonly IAdvancedBus _bus;
        private readonly IExchange _exchange;
        private readonly IQueue _queue;
        private IDisposable _consumer;
        private readonly IMessageConsumer _messageConsumer;
        public MicroServiceLogic(IRuntimeEnvironment env, 
            IApplicationEnvironment app,
            IAdvancedBus bus,
            IMessageConsumer messageConsumer, 
            IExchange exchange,
            IQueue queue)
        {
           _env = env;
           _app = app;
           _queue = queue;
           _messageConsumer = messageConsumer;
           _exchange = exchange;
           _bus = bus;
           
        }
        public void Start()
        {
            Console.WriteLine("Hello World");
            Console.WriteLine($@"
                IApplicationEnvironment:
                        Base Path:      {_app.ApplicationBasePath}
                        App Name:       {_app.ApplicationName}
                        App Version:    {_app.ApplicationVersion}
                        Runtime:        {_app.RuntimeFramework}
                IRuntimeEnvironment:
                        OS:             {_env.OperatingSystem}
                        OS Version:     {_env.OperatingSystemVersion}
                        Architecture:   {_env.RuntimeArchitecture}
                        Path:           {_env.RuntimePath}
                        Type:           {_env.RuntimeType}
                        Version:        {_env.RuntimeVersion}");
            Log.Information("Info");
            
             Log.Information("Event=\"Binding message queue to exchange\" Exchange=\"{0}\" Queue=\"{1}\" ", _exchange.Name, _queue.Name);
            _bus.Bind(_exchange, _queue, "A.*");

            Log.Information("Event=\"Subscribing to message queue\" Queue=\"{0}\" ", _queue.Name);
             _consumer =  _bus.Consume<object>(_queue, (message, info) =>
                Task.Factory.StartNew(() =>
                    _messageConsumer.Consume(message)
                    )
                );
            
            Console.Read();
        }
    }
}