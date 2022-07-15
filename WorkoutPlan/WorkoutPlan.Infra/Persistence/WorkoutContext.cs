using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace WorkoutPlan.Infra.Persistence;

public class WorkoutContext : DbContext
{
    public const string DEFAULT_SCHEMA = "public";
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}