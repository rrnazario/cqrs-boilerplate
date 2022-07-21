using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WorkoutPlan.Application.DI
{
    public static class MessagingInjection
    {
        class MessagingOptions
        {
            public string Host { get; set; }
            public string User { get; set; }
            public string Password { get; set; }

            public MessagingOptions() { }
        }

        public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(cfg => cfg.ConfigureDefaultRabbitMQ(configuration));

            return services;
        }

        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(typeof(MessagingInjection).Assembly);

            return services;
        }


        public static void ConfigureDefaultRabbitMQ(this IBusRegistrationConfigurator c, IConfiguration configuration)
            => c.UsingRabbitMq((c, r) => AddDefaultSettingsRabbitMQ(c, r, configuration));

        private static void AddDefaultSettingsRabbitMQ(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator rbtConfig, IConfiguration configuration)
        {
            var messagingOptions = configuration.GetSection("Messaging")
                .Get<MessagingOptions>(a => a.BindNonPublicProperties = true);

            rbtConfig.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(30)));
            rbtConfig.UseDelayedRedelivery(r => r.Interval(5, TimeSpan.FromSeconds(30)));

            rbtConfig.Host(messagingOptions.Host, "/", hostConfig =>
            {
                hostConfig.Username(messagingOptions.User);
                hostConfig.Password(messagingOptions.Password);
            });

            rbtConfig.AutoStart = true;

            rbtConfig.UseInMemoryOutbox();

            rbtConfig.ConfigureEndpoints(context);
        }
    }
}