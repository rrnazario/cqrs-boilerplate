using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using MediatR;

namespace WorkoutPlan.Application.DI
{
    public static class MessagingInjection
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetSection("Messaging")
                .Get<MessagingOptions>(a => a.BindNonPublicProperties = true);

            services.AddMassTransit(cfg =>
            {
                cfg.UsingRabbitMq((ctx, rbtConfig) =>
                {
                    rbtConfig.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(30)));
                    rbtConfig.UseDelayedRedelivery(r => r.Interval(5, TimeSpan.FromSeconds(30)));

                    rbtConfig.Host(config.Host, "/", hostConfig =>
                    {
                        hostConfig.Username(config.User);
                        hostConfig.Password(config.Password);
                    });

                    rbtConfig.AutoStart = true;

                    rbtConfig.UseInMemoryOutbox();

                    rbtConfig.ConfigureEndpoints(ctx);
                });
            });

            return services;
        }

        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(typeof(MessagingInjection).Assembly);

            return services;
        }
    }

    public class MessagingOptions
    {
        public string Host { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}