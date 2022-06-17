using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutPlan.Domain.AggregatesModel.WorkoutSheetAggregate;
using WorkoutPlan.Domain.SeedWork;

namespace WorkoutPlan.Domain.Events.Worksheets
{
    public record WorksheetExerciseAddedDomainEvent(Exercise exercise) : IDomainEvent;    
}
