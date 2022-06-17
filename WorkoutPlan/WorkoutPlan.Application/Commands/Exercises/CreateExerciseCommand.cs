using Marten;
using MediatR;
using Light.GuardClauses;
using Exercise = WorkoutPlan.Domain.AggregatesModel.WorkoutSheetAggregate.Exercise;
using WorkoutPlan.Application.Events.Exercises;
using MassTransit;

namespace WorkoutPlan.Application.Commands.Exercises
{
    public record CreateExerciseCommand : IRequest<Guid>
    {
        public CreateExerciseCommand(string name, string description, List<string> medias)
        {
            Name = name;
            Description = description;
            Medias = medias;
        }

        public string Name { get; }
        public string Description { get; }
        public List<string> Medias { get; }
    }

    public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand, Guid>
    {
        private readonly IDocumentStore _store;
        private readonly IBus _bus;
        public CreateExerciseCommandHandler(IDocumentStore store, 
                                            IBus bus)
        {
            _store = store.MustNotBeNull();
            _bus = bus.MustNotBeNull();
        }

        public async Task<Guid> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            using var session = _store.OpenSession();

            var exercise = new Exercise(request.Name, request.Description, request.Medias);

            session.Events.Append(exercise.Id, exercise.UncommitedEvents.ToArray());

            await session.SaveChangesAsync(cancellationToken);

            exercise.ClearUncommitedEvents();

            var integrationEvent = new ExerciseCreatedIntegrationEvent(exercise.Id);

            await _bus.Publish(integrationEvent);

            return exercise.Id;
        }
    }
}
