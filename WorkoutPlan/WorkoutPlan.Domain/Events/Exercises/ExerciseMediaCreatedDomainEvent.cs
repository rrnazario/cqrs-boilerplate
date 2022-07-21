namespace WorkoutPlan.Domain.Events.Exercises
{
    public record ExerciseMediaCreatedDomainEvent(string Media) : IDomainEvent;
}
