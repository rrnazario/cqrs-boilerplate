using MassTransit;
using WorkoutPlan.Application.DI;
using WorkoutPlan.Service.Consumers;
using WorkoutPlan.Service.HostedService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHostedService<SimpleReceiver>();

var config = builder.Configuration.GetSection("Messaging")
                .Get<MessagingOptions>(a => a.BindNonPublicProperties = true);

builder.Services.AddMassTransit(cfg =>
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

        rbtConfig.ConfigureEndpoints(ctx);
    });

    cfg.AddConsumer<ExerciseCreatedConsumer>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
