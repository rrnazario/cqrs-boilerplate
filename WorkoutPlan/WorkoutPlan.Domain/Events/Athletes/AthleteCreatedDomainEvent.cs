using WorkoutPlan.Domain.SeedWork;

namespace WorkoutPlan.Domain.Events.Athletes
{
    public record AthleteCreatedDomainEvent(
        string Name
    ) : IDomainEvent;
}
