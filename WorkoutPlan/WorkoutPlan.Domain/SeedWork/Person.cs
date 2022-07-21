namespace WorkoutPlan.Domain.SeedWork
{
    public abstract record Person
        : Entity
    {
        protected Person(string name)
        {
            Name = name.MustNotBeNullOrEmpty(DomainException.Throw);
        }

        public string Name { get; protected set; }
    }
}
