using WorkoutPlan.Domain.SeedWork;

namespace WorkoutPlan.Domain.Events.Exercises
{
    public record EventMediaCreatedDomainEvent(string Media) : IDomainEvent;
}
