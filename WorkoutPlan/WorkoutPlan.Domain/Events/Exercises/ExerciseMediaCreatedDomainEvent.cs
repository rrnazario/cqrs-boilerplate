using WorkoutPlan.Domain.SeedWork;

namespace WorkoutPlan.Domain.Events.Exercises
{
    public record ExerciseMediaCreatedDomainEvent(string Media) : IDomainEvent;
}
