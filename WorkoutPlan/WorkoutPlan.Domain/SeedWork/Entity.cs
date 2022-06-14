using System.Text.Json.Serialization;

namespace WorkoutPlan.Domain.SeedWork
{
    public abstract record Entity
    {
        [JsonIgnore]
        private List<IDomainEvent> _uncommitedEvents;
        
        [JsonIgnore]
        public IReadOnlyList<IDomainEvent> UncommitedEvents => _uncommitedEvents.AsReadOnly();
        public Guid Id { get; }

        public Entity()
        {
            _uncommitedEvents = new();
        }

        public void AddEvent(IDomainEvent evt) => _uncommitedEvents.Add(evt);
        public void ClearEvents() => _uncommitedEvents.Clear();
    }
}
