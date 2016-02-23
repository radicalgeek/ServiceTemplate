using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Framework.DependencyInjection;
using Autofac;
using Serilog;
using System.ServiceProcess;
//using Service.Logging;

namespace Service.Host
{
    public class Program 
    {
        public Program()
        {
            
        }
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                                .WriteTo.TextWriter(Console.Out)
                                .CreateLogger();
                                
            var container = DependancyContainer.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                Log.Information("Starting Service {ServiceName}", "Template Service");
                var service = scope.Resolve<IHostableMicroService>();
                service.Run();
            }
        }
    }
}
