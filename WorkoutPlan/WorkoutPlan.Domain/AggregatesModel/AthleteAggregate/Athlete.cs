namespace WorkoutPlan.Domain.AggregatesModel.AthleteAggregate
{
    public record Athlete
        : Person, IAggregateRoot
    {
        private Athlete() : base("") { }
        
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
