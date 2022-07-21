using WorkoutPlan.Domain.AggregatesModel.WorkoutSheetAggregate;

namespace WorkoutPlan.Domain.Events.Worksheets
{
    public record WorksheetExerciseAddedDomainEvent(Exercise exercise) : IDomainEvent;    
}
