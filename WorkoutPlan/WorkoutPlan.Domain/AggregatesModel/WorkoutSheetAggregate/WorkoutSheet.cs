using System.Text.Json.Serialization;
using WorkoutPlan.Domain.SeedWork;

namespace WorkoutPlan.Domain.AggregatesModel.WorkoutSheetAggregate
{
    public record WorkoutSheet
        : Entity, IAggregateRoot
    {

        [JsonIgnore]
        private List<Exercise> _exercises;        
        public IReadOnlyList<Exercise> Exercises => _exercises.AsReadOnly();

        public Athlete Athlete { get; }

        public Teacher Teacher { get; }


        protected WorkoutSheet() : base()
        {
            _exercises = new();
        }


        public void AddExercise(Exercise exercise)
        {
            //TODO:Validations

            _exercises.Add(exercise);

            //TODO: Add exercise added event
        }


    }
}
