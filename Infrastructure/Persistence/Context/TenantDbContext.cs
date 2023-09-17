using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Tenant> Tenants => Set<Tenant>();
    }
}