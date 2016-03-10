
using Microsoft.Extensions.Configuration;

namespace Service.Host
{
    public static class Configuration
    {
        public static IConfigurationRoot Build()
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
            return config;
        }
    }
}