using System.Text.Json.Serialization;

namespace WorkoutPlan.Domain.SeedWork
{
    public abstract record Entity
    {
        [JsonIgnore]
        private List<IDomainEvent> _uncommitedEvents;
        
        [JsonIgnore]
        public IReadOnlyList<IDomainEvent> UncommitedEvents => _uncommitedEvents.AsReadOnly();
        public Guid Id { get; protected set; }
        public int Version { get; protected set; } = 0;

        public Entity()
        {
            _uncommitedEvents = new();
        }

        public void AddUncommitedEvent(IDomainEvent evt) => _uncommitedEvents.Add(evt);
        public void ClearUncommitedEvents() => _uncommitedEvents.Clear();
    }
}
