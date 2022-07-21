namespace WorkoutPlan.Application.DI
{
    public static class LoggingInjection
    {
        public static IHostBuilder AddSerilog(this IHostBuilder host)
        {
            host.UseSerilog((ctx, lc) =>
            {
                //var conn = ctx.Configuration.GetConnectionString("loki");
                lc.Enrich.FromLogContext()
                    .Enrich.WithProperty("Application", ctx.HostingEnvironment.ApplicationName)
                    .Enrich.WithProperty("Environment", ctx.HostingEnvironment.EnvironmentName)
                    .WriteTo.Console()
                    //.WriteTo.GrafanaLoki(conn)
                    ;
            });

            return host;
        }
    }
}
