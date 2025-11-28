using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace LineTranslateBot
{
    class Program
    {
        static async Task Main()
        {
            var builder = new HostBuilder();
            builder.ConfigureWebJobs(b =>
            {
                b.AddAzureStorageBlobs();
                b.AddAzureStorageQueues();
            });
            var host = builder.Build();
            using (host)
            {
                await host.RunAsync();
            }
        }
    }
}
