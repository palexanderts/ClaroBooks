using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHttpClient(string.Empty, c =>
            {
                c.Timeout = TimeSpan.FromSeconds(10);
                c.BaseAddress = new Uri(configuration.GetConnectionString("fakerest"));
            });
        }
    }
}
