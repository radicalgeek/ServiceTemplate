using System;
using Service.Logging;

namespace Service.Host
{
    public class MicroServiceLogic : IHostableMicroService
    {
        private ILogger _logger;
        public MicroServiceLogic(ILogger logger)
        {
            _logger = logger;
        }
        public void Run()
        {
            Console.WriteLine("Hello World");
            _logger.Info("Info");
            Console.Read();
        }
    }
}