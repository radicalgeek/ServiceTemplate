
using Microsoft.Extensions.Configuration;
using Service.Data.PostgreSql;

namespace Service.Host
{
    public static class Configuration
    {
        public static IConfigurationRoot Build()
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("../config.json")
                .AddNpgsql()
                .AddDbContext<PostgreDbContext>()
                .Build();
            return config;
        }
    }
}