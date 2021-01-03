using Microsoft.Extensions.Configuration;

namespace NPista.Data.EFCore.Helpers
{
    public class ConnectionHelper
    {
        public static string GetConnectionString() => new ConfigurationBuilder()
            .AddJsonFile("settings.json")
            .Build().GetConnectionString("NPista");
    }
}
