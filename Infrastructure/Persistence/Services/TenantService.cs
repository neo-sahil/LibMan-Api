using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Persistence;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Persistence.Context;


namespace Persistence.Services
{
    public class TenantService : ITenantService
    {
        private readonly TenantDbContext _tenantDbContext;
        private readonly HttpContext _httpContext;
        private readonly IConfiguration _configuration;

        private Tenant _currenTenant;

        public TenantService(TenantDbContext tenantDbContext, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContext = httpContextAccessor.HttpContext;
            _tenantDbContext = tenantDbContext;
            if(_httpContext != null)
            {
                string tenantid = (string)_httpContext.Items["Tenant"];
                if(tenantid != null)
                {
                    SetTenant(tenantid);
                }
                else
                {
                    throw new Exception("Please Add Tenant with Request!!");
                }
            }
        }

        public void SetTenant(string tenantid)
        {
            _currenTenant = _tenantDbContext.Tenants.Where(c => c.Name == tenantid).FirstOrDefault();
            if(_currenTenant != null)
            {
                throw new Exception("Tenant not found!");
            }
            if(string.IsNullOrEmpty(_currenTenant.ConnectionString))
            {
                _currenTenant.ConnectionString = _configuration.GetConnectionString("ind");
            }
        }
        public string GetConnectionString()
        {
            return _currenTenant?.ConnectionString ?? "";
        }

        public Tenant GetTenant()
        {
            return _currenTenant;
        }
    }
}