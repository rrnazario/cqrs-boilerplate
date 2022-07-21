namespace WorkoutPlan.Application.Commands.Exercises
{
    public record CreateExerciseCommand(
            string Name,
            string Description,
            List<string> Medias
        ) : IRequest<Guid>
    { }


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

            await _bus.Publish(integrationEvent, cancellationToken);

            return exercise.Id;
        }
    }
}
