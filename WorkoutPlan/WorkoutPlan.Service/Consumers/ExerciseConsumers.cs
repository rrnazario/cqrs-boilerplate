namespace WorkoutPlan.Service.Consumers
{
    public class ExerciseCreatedConsumer : IConsumer<ExerciseCreatedIntegrationEvent>
    {
        private readonly ILogger<ExerciseCreatedConsumer> _logger;

        public ExerciseCreatedConsumer(ILogger<ExerciseCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<ExerciseCreatedIntegrationEvent> context)
        {
            _logger.LogInformation("Exercise {Id} successfully created. Received on '{Name}'",
                                   context.Message.Id,
                                   typeof(ExerciseCreatedConsumer).Assembly.GetName().Name);

            return Task.CompletedTask;
        }
    }
}
