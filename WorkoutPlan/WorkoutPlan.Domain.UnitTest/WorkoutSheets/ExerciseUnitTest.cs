namespace WorkoutPlan.Domain.UnitTest.WorkoutSheets
{
    public class ExerciseUnitTest
    {
        [InlineData(null, null)]
        [InlineData("", "valid")]
        [InlineData("valid", "")]
        [Theory]
        public void GivenInvalidData_WhenAttemptsToCreateAnExercise_ThenThrows(string name, string description)
        {
            var action = () => new Exercise(name, description, default);

            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void GivenMediasAlreadyAdded_WhenAttemptsToAddThem_ThenNothingIsAdded()
        {
            var medias = new List<string>() { "Media", "Media" };

            var exercise = new Exercise("name", "description", medias);

            exercise.Medias.Should().HaveCount(1);
        }

    }
}
