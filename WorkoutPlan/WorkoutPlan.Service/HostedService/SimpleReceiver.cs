namespace WorkoutPlan.Service.HostedService
{
    public class SimpleReceiver : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromHours(1));
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
            => await Task.Delay(Timeout.Infinite, cancellationToken);
    }
}