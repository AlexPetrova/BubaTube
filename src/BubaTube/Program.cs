using BubaTube.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace BubaTube
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .SeedRoles()
                .Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
