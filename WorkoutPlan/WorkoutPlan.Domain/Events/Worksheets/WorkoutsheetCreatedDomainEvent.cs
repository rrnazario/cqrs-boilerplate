using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutPlan.Domain.SeedWork;

namespace WorkoutPlan.Domain.Events.Worksheets
{
    public record WorkoutsheetCreatedDomainEvent(
        Guid athleteId,
        Guid teacherId
    ) : IDomainEvent;
}
