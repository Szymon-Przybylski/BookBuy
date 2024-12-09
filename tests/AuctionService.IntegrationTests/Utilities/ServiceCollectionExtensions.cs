using AuctionService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionService.IntegrationTests.Utilities
{
    public static class ServiceCollectionExtensions
    {
        public static void RemoveDatabaseContext<T>(this IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<AuctionDatabaseContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
        }

        public static void PrepareDatabaseForTesting<T>(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AuctionDatabaseContext>();

            db.Database.Migrate();
            DatabaseHelper.InitializeDatabaseForTesting(db);
        }
    }
}
