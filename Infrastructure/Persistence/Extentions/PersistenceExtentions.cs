using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Context;
using Persistence.Helper;

namespace Persistence.Extentions
{
    public static class PersistenceExtentions
    {
        public async static void AddMultiTenantAsync(this IServiceCollection services, IConfiguration configuration, List<Tenant> tenants)
        {
            foreach (var tenant in tenants)
            {
                string connectionString = "";
                if(string.IsNullOrEmpty(tenant.ConnectionString))
                {
                    connectionString = configuration.GetConnectionString("ind");
                }
                else
                {
                    connectionString = tenant.ConnectionString;
                }

                using var scope =  services.BuildServiceProvider().CreateAsyncScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.SetConnectionString(connectionString);
                if (dbContext.Database.GetMigrations().Count() > 0)
                {
                    await dbContext.Database.MigrateAsync();
                }
                Seeding.SeedUserProfile(dbContext, configuration);
                Seeding.SeedUsers(dbContext, configuration);
            }
        }
    }
}