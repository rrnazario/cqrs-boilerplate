using Light.GuardClauses.Exceptions;
using WorkoutPlan.Domain.AggregatesModel.AthleteAggregate;

namespace WorkoutPlan.Domain.UnitTest.Athletes
{
    public class AthleteUnitTest
    {
        [InlineData(null)]
        [InlineData("")]
        [Theory]
        public void GivenAnInvalidName_WhenAttemptsToCreateAnAthlete_ThenThrows(string name)
            => Assert.Throws<ArgumentException>(() => new Athlete(name));
        
    }
}