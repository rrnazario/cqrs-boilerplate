namespace WorkoutPlan.Domain.UnitTest.WorkoutSheets
{
    public class WorkoutSheetUnitTest
    {
        [Fact]
        public void WhenCreatingAnWorkSheet_ThenProperDomainEventsWereCreated()
        {
            var worksheet = new WorkoutSheet(Guid.NewGuid(),
                                             Guid.NewGuid(), 
                                             new() 
                                             { 
                                                 new("Exercise", "Descrption", new(){ "" })
                                             });

            worksheet.UncommitedEvents.Should().HaveCount(2);
            worksheet.UncommitedEvents.First().Should().BeOfType(typeof(WorkoutsheetCreatedDomainEvent));
            worksheet.UncommitedEvents.Last().Should().BeOfType(typeof(WorksheetExerciseAddedDomainEvent));
        }
    }
}
