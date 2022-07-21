using WorkoutPlan.Domain.Events.Worksheets;

namespace WorkoutPlan.Domain.AggregatesModel.WorkoutSheetAggregate
{
    public record WorkoutSheet
        : Entity, IAggregateRoot
    {

        [JsonIgnore]
        private List<Exercise> _exercises;        
        public IReadOnlyList<Exercise> Exercises => _exercises.AsReadOnly();

        public Guid AthleteId { get; private set; }

        public Guid TeacherId { get; private set; }


        protected WorkoutSheet() : base()
        {
            _exercises = new();
        }

        public WorkoutSheet(Guid athleteId, Guid teacherId, List<Exercise> exercises)  : this()
        {
            athleteId.MustNotBe(Guid.Empty).MustNotBeNullReference();
            teacherId.MustNotBe(Guid.Empty).MustNotBeNullReference();
            exercises.MustNotBeNullOrEmpty();

            var newWorkoutEvent = new WorkoutsheetCreatedDomainEvent(athleteId, teacherId);
            AddUncommitedEvent(newWorkoutEvent);
            Apply(newWorkoutEvent);

            foreach (var exercise in exercises)
                AddExercise(exercise);
        }

        private void Apply(WorkoutsheetCreatedDomainEvent evt)
        {
            TeacherId = evt.teacherId;
            AthleteId = evt.athleteId;

            Version++;
        }

        public void AddExercise(Exercise exercise)
        {
            if (!_exercises.Any(_ => _.Id == exercise.Id))
            {
                var addedEvent = new WorksheetExerciseAddedDomainEvent(exercise);

                Apply(addedEvent);
                AddUncommitedEvent(addedEvent);
            }
        }

        private void Apply(WorksheetExerciseAddedDomainEvent evt)
        {
            _exercises.Add(evt.exercise);
            Version++;
        }
    }
}
