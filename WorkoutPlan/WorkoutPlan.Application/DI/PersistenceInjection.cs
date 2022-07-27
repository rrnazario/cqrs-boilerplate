using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkoutPlan.Infra.Persistence;

namespace WorkoutPlan.Application.DI
{
    public static class PersistenceInjection
    {
        class PersistenceConfig
        {
            public string ConnectionString { get; set; }
            public string Owner { get; set; }
        }
        
        public static IServiceCollection AddEventStorePersistence(this IServiceCollection services, IConfiguration configuration)
        {           
            var config = configuration.GetSection("ESPersistence")
                .Get<PersistenceConfig>(a => a.BindNonPublicProperties = true);

            services.AddMarten(cfg =>
            {
                cfg.Connection(config.ConnectionString);
                cfg.DatabaseSchemaName = "workoutPlan";
                cfg.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.CreateOrUpdate;

                //TODO: Modify aggregation root entitiy mappers
                
                //only for debug
                cfg.CreateDatabasesForTenants(provider =>
                {
                    provider.ForTenant().
                    CheckAgainstPgDatabase().
                    WithOwner(config.Owner).
                    WithEncoding("UTF-8").
                    ConnectionLimit(-1);
                });
            });

            return services;
        }

        public static IServiceCollection AddEntityFrameworkPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            var config = configuration.GetSection("EFPersistence")
                .Get<PersistenceConfig>(a => a.BindNonPublicProperties = true);

            services.AddEntityFrameworkNpgsql().AddDbContext<WorkoutContext>(op =>
            {
                op.UseNpgsql(config.ConnectionString);
            });
            
            return services;
        }
        
    }


}
