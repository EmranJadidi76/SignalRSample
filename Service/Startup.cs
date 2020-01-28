using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public static class Startup
    {
        public static void DatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options
                => options.UseSqlServer(configuration.GetConnectionString("MyConnection")));

            services.Scan(scan =>
                scan.FromAssemblyOf<DatabaseContext>()
                    .AddClasses(classes => classes.InNamespaceOf<DatabaseContext>())
                    .AsSelf()
                    .WithScopedLifetime());

            #region Mapper
            services.AddAutoMapper(typeof(UserRoleMapper));
            #endregion
        }

    }
}
