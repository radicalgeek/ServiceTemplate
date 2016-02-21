using System;
//using Service.Logging;
using Serilog;

namespace Service.Host
{
    public class MicroServiceLogic : IHostableMicroService
    {
        public MicroServiceLogic()
        {
           
        }
        public void Run()
        {
            Console.WriteLine("Hello World");
            Log.Information("Info");
            Console.Read();
        }
    }
}