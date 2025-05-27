using Diana.Core.Defaults;
using Microsoft.Extensions.Hosting;

namespace Diana
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            DianaApplicationBuilder builder = DianaApplicationBuilder.Create(args);
            IHost host = builder.Build();
            await host.StartAsync();
            await Task.Delay(-1);   
        }
    }
}
