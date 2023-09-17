using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ITenantService _tenantService;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions, ITenantService tenantService) : base(dbContextOptions)
        {
            _tenantService = tenantService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var tenantConnectionString = _tenantService.GetConnectionString();
            if (!string.IsNullOrEmpty(tenantConnectionString))
            {
                optionsBuilder.UseSqlServer(tenantConnectionString);
            }
        }

        public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Test> Tests => Set<Test>();
    }
}