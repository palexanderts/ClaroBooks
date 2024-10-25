using Data.Repositories;
using Domain.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace Data
{
    public static class DependencyInjection
    {
        public static void AddDomainServices(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddScoped<ServiceBoocks>();
        }

        public static void AddDataServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IRepositoryBoocks, RepositoryBooks>();

            services.AddDbContext<BoocksDbContext>(options =>
                    options.UseSqlite(configuration.GetConnectionString("DefaultConnection"))
                           .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        }
    }
}
