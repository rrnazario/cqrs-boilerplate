using FluentAssertions;
using Marten;
using Marten.Events;
using MassTransit;
using Moq;
using System.Data;
using WorkoutPlan.Application.Commands.Exercises;

namespace WorkoutPlan.Application.UnitTest.Commands.Exercises
{
    public class ExerciseCommandTests
    {
        [Fact]
        public async Task GivenProperParameters_WhencreatingAnExerciseFromCommand_RunsSuccessfully()
        {
            //Arrange
            var command = new CreateExerciseCommand("pullover", "Do a pullover", new());

            var target = CreateCreateExerciseCommandHandler();

            //Act
            var result = await target.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBe(Guid.Empty);
        }

        private CreateExerciseCommandHandler CreateCreateExerciseCommandHandler(
            IDocumentStore? documentStore = null,
            IBus? bus = null)
        {
            return new(documentStore ?? CreateDocumentStoreMock(),
                       bus ?? new Mock<IBus>().Object);
        }

        private IDocumentStore CreateDocumentStoreMock()
        {
            var docStoreMock = new Mock<IDocumentStore>();
            var docSessionMock = new Mock<IDocumentSession>();
            var eventStoreMock = new Mock<IEventStore>();

            docSessionMock.SetupGet(s => s.Events).Returns(eventStoreMock.Object);

            docStoreMock
                .Setup(s => s.OpenSession(It.IsAny<DocumentTracking>(), It.IsAny<IsolationLevel>()))
                .Returns(docSessionMock.Object);

            return docStoreMock.Object;
        }
    }
}
