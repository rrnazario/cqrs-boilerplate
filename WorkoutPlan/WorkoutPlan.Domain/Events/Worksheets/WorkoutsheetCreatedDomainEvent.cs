using WorkoutPlan.Domain.SeedWork;

namespace WorkoutPlan.Domain.Events.Worksheets
{
    public record WorkoutsheetCreatedDomainEvent(
        Guid athleteId,
        Guid teacherId
    ) : IDomainEvent;
}
