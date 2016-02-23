using System;
using Microsoft.Extensions.PlatformAbstractions;
using Serilog;

namespace Service.Host
{
    public class MicroServiceLogic : IHostableMicroService
    {
        private IRuntimeEnvironment _env;
        private IApplicationEnvironment _app;
        public MicroServiceLogic(IRuntimeEnvironment env, IApplicationEnvironment app)
        {
           _env = env;
           _app = app;
        }
        public void Run()
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
            Console.Read();
        }
    }
}