namespace WorkoutPlan.Domain.AggregatesModel.WorkoutSheetAggregate
{
    public record Exercise 
        : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        
        private List<string> _medias;
        
        [JsonIgnore]
        public IReadOnlyCollection<string> Medias => _medias.AsReadOnly();

        private Exercise() { _medias = new(); }
        public Exercise(string name, string description, List<string> medias) : this()
        {
            name.MustNotBeNullOrEmpty();
            description.MustNotBeNullOrEmpty();
            medias.MustNotBeNullOrEmpty();

            foreach (var media in medias)            
                AddMedia(media);            

            var evt = new ExerciseCreatedDomainEvent(Guid.NewGuid(), name, description, _medias);
            
            AddUncommitedEvent(evt);
            Apply(evt);
        }

        private void AddMedia(string media)
        {
            if (!string.IsNullOrEmpty(media))
            {
                var newMediaEvent = new EventMediaCreatedDomainEvent(media);
                AddUncommitedEvent(newMediaEvent);
                Apply(newMediaEvent);
            }
        }

        private void Apply(EventMediaCreatedDomainEvent newMediaEvent)
        {
            _medias.Add(newMediaEvent.Media);

            Version++;
        }

        private void Apply(ExerciseCreatedDomainEvent evt)
        {
            Id = evt.Id;
            Name = evt.Name;
            Description = evt.Description;
            _medias = evt.Medias;

            Version++;
        }
    }
}
