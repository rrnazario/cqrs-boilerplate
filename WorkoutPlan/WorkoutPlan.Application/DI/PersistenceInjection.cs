﻿using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WorkoutPlan.Application.DI
{
    public static class PersistenceInjection
    {
        class PersistenceConfig
        {
            public string ConnectionString { get; set; }
        }
        
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {           
            var config = configuration.GetSection("Persistence")
                .Get<PersistenceConfig>(a => a.BindNonPublicProperties = true);

            services.AddMarten(cfg =>
            {
                cfg.Connection(config.ConnectionString);
                cfg.DatabaseSchemaName = "workoutPlan";
                cfg.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.CreateOrUpdate;

                //TODO: Modify aggregation root entitiy mappers
            });

            return services;
        }


    }


}