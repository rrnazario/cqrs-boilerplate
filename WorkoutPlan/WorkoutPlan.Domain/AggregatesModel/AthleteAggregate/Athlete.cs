using WorkoutPlan.Domain.Events.Athletes;
using WorkoutPlan.Domain.SeedWork;

namespace WorkoutPlan.Domain.AggregatesModel.AthleteAggregate
{
    public record Athlete
        : Person, IAggregateRoot
    {
        public Athlete(string name) : base(name) 
        {
            var evt = new AthleteCreatedDomainEvent(name);
            AddUncommitedEvent(evt);
            Apply(evt);

        }

        private void Apply(AthleteCreatedDomainEvent evt)
        {
            Name = evt.Name;
            Version++;
        }
    }
}
