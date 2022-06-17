using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutPlan.Domain.SeedWork;

namespace WorkoutPlan.Domain.Events.Exercises
{
    public record EventMediaCreatedDomainEvent(string Media) : IDomainEvent;
}
