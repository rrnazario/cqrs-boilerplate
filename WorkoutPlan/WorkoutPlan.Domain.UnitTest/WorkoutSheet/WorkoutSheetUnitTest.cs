using FluentAssertions;
using WorkoutPlan.Domain.Events.Worksheets;
using WorkoutSheet = WorkoutPlan.Domain.AggregatesModel.WorkoutSheetAggregate.WorkoutSheet;

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
            Assert.True(worksheet.UncommitedEvents.Count(w => w.GetType() == typeof(WorkoutsheetCreatedDomainEvent)) == 1);
            Assert.True(worksheet.UncommitedEvents.Count(w => w.GetType() == typeof(WorksheetExerciseAddedDomainEvent)) == 1);
        }
    }
}
