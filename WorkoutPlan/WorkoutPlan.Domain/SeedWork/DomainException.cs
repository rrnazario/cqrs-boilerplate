namespace WorkoutPlan.Domain.SeedWork
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }

        public static DomainException Throw(string? _) => new(_);
    }
}
