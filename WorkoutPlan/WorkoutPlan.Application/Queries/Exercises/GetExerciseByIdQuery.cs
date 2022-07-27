using Microsoft.Extensions.Logging;

namespace WorkoutPlan.Application.Queries.Exercises
{
    public record GetExerciseByIdQuery(
        Guid Id
    ) : IRequest<ExerciseQueryResponse>;

    public record ExerciseQueryResponse(string Name, string Description, IReadOnlyCollection<string> Medias);

    public class GetExerciseByIdQueryHandler :
        IRequestHandler<GetExerciseByIdQuery, ExerciseQueryResponse>
    {
        private readonly IDocumentStore _store;
        private readonly ILogger<GetExerciseByIdQueryHandler> _logger;

        public GetExerciseByIdQueryHandler(IDocumentStore store,
            ILogger<GetExerciseByIdQueryHandler> logger)
        {
            _store = store;
            _logger = logger;
        }

        public async Task<ExerciseQueryResponse> Handle(GetExerciseByIdQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving information for Exercise {Id}...", request.Id);

            using var session = _store.QuerySession();

            var agg = await session.LoadAsync<Exercise>(request.Id, cancellationToken);
            var version = agg?.Version + 1 ?? 0;

            var result = await session.Events.AggregateStreamAsync(request.Id, state: agg, fromVersion: version,
                token: cancellationToken);

            return result != null
                ? new(result.Name, result.Description, result.Medias)
                : default;
        }
    }
}