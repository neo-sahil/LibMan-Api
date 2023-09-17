using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Services.Persistence
{
    public interface ITenantService
    {
        public Tenant GetTenant();
        public string GetConnectionString();
    }
}