using Microsoft.AspNetCore.Hosting;


[assembly: HostingStartup(typeof(FinanceApp.Areas.Identity.IdentityHostingStartup))]
namespace FinanceApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}