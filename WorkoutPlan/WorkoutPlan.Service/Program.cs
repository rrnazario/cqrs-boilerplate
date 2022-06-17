using MassTransit;
using WorkoutPlan.Application.DI;
using WorkoutPlan.Service.Consumers;
using WorkoutPlan.Service.HostedService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHostedService<SimpleReceiver>();

builder.Services.AddMassTransit(cfg =>
{
    cfg.ConfigureDefaultRabbitMQ(builder.Configuration);

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
