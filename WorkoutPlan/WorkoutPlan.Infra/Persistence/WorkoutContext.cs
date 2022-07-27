namespace WorkoutPlan.Infra.Persistence;

public class WorkoutContext : DbContext
{
    public const string DEFAULT_SCHEMA = "public";

    public WorkoutContext(DbContextOptions<WorkoutContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}