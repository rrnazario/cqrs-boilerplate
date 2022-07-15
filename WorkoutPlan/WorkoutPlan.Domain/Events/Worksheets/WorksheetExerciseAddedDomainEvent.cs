using WorkoutPlan.Domain.AggregatesModel.WorkoutSheetAggregate;
using WorkoutPlan.Domain.SeedWork;

namespace WorkoutPlan.Domain.Events.Worksheets
{
    public record WorksheetExerciseAddedDomainEvent(Exercise exercise) : IDomainEvent;    
}
