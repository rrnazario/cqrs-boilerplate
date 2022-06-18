using FluentAssertions;
using WorkoutPlan.Domain.AggregatesModel.AthleteAggregate;

namespace WorkoutPlan.Domain.UnitTest.Athletes
{
    public class AthleteUnitTest
    {
        [InlineData(null)]
        [InlineData("")]
        [Theory]
        public void GivenAnInvalidName_WhenAttemptsToCreateAnAthlete_ThenThrows(string name)
        {
            var action = () => new Athlete(name);

            action.Should().Throw<ArgumentException>();
        }
        
    }
}