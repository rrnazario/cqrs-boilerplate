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
            name.MustNotBeNullOrEmpty(DomainException.Throw);
            description.MustNotBeNullOrEmpty(DomainException.Throw);

            medias?.ForEach(AddMedia);

            var evt = new ExerciseCreatedDomainEvent(Guid.NewGuid(), name, description, _medias);

            AddUncommitedEvent(evt);
            Apply(evt);
        }

        private void AddMedia(string media)
        {
            if (!string.IsNullOrEmpty(media) && !_medias.Contains(media))
            {
                var newMediaEvent = new ExerciseMediaCreatedDomainEvent(media);
                AddUncommitedEvent(newMediaEvent);
                Apply(newMediaEvent);
            }
        }

        private void Apply(ExerciseMediaCreatedDomainEvent newMediaEvent)
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
