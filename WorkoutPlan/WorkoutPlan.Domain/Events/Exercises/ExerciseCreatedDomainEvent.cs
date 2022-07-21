namespace WorkoutPlan.Application.Events.Exercises
{
    public record ExerciseCreatedDomainEvent(
        Guid Id,
        string Name,
        string Description,
        List<string> Medias
    ) : IDomainEvent;
}
