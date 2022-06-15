using WorkoutPlan.Domain.SeedWork;

namespace WorkoutPlan.Domain.AggregatesModel.WorkoutSheetAggregate
{
    public record Exercise 
        : Entity
    {
        public string Name { get; }
        public string Description { get; }
        
        private List<string> _medias;
        public IReadOnlyCollection<string> Medias => _medias.AsReadOnly();

        public Exercise(string name, string description, List<string> medias)
        {
            Name = name;
            Description = description;
            _medias = medias;
        }
    }
}
