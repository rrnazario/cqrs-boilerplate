namespace WorkoutPlan.Domain.AggregatesModel.TeacherAggregate
{
    public record Teacher
        : Person, IAggregateRoot
    {
        public Teacher(string name) : base(name)
        {
            //TODO: validations
        }
    }
}
