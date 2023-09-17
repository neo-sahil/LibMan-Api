global using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
//using Migrations;
using Persistence.Context;
using Persistence.Extentions;
using Persistence.Helper;
using Persistence.Services;

namespace Persistence
{
    public static class PersistenceStartUp
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<TenantDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ind"), b => b.MigrationsAssembly(typeof(PersistenceStartUp).Assembly.FullName)));

            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TenantDbContext>();
            if (dbContext.Database.GetMigrations().Count() > 0)
            {
                dbContext.Database.MigrateAsync();
            }
            Seeding.SeedTenant(dbContext, configuration);

            services.AddScoped<ITenantService, TenantService>();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ind"), b => b.MigrationsAssembly(typeof(PersistenceStartUp).Assembly.FullName)));

            var tenants = dbContext.Tenants.ToList();
            services.AddMultiTenantAsync(configuration, tenants);

        }
    }
}