namespace WorkoutPlan.Domain.SeedWork
{
    public abstract record Person
        : Entity
    {
        protected Person(string name)
        {
            Name = name.MustNotBeNullOrEmpty(_ => new ArgumentException(nameof(Name)));
        }

        public string Name { get; protected set; }
    }
}
