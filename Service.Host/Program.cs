using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Framework.DependencyInjection;
using Autofac;
using Service.Logging;

namespace Service.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var container = DependancyContainer.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IHostableMicroService>();
                service.Run();
            }
        }
    }
}
