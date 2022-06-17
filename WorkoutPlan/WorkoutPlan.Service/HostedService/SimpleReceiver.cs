namespace WorkoutPlan.Service.HostedService
{
    public class SimpleReceiver : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
                await Task.Delay(TimeSpan.FromSeconds(2));
        }
    }
}
