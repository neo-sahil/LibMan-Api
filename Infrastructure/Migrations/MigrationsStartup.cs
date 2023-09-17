using Microsoft.Extensions.DependencyInjection;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Migrations
{
    public static class MigrationsStartup
    {
        public static void AddMigration(this IServiceCollection services)
        {
            var ss = typeof(MigrationsStartup).Assembly.FullName;
            //Migrations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        }
    }
}