using Marten;
using MediatR;
using WorkoutPlan.Domain.AggregatesModel.WorkoutSheetAggregate;

namespace WorkoutPlan.Application.Queries.Exercises
{
    public class GetExerciseByIdQuery : IRequest<ExerciseQueryResponse>
    {
        public GetExerciseByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }

    public record ExerciseQueryResponse(string Name, string Description, IReadOnlyCollection<string> Medias);

    public class GetExerciseByIdQueryHandler :
        IRequestHandler<GetExerciseByIdQuery, ExerciseQueryResponse>
    {
        private readonly IDocumentStore _store;

        public GetExerciseByIdQueryHandler(IDocumentStore store)
        {
            _store = store;
        }

        public async Task<ExerciseQueryResponse> Handle(GetExerciseByIdQuery request, CancellationToken cancellationToken)
        {
            using var session = _store.QuerySession();

            var agg = await session.LoadAsync<Exercise>(request.Id);
            var version = agg?.Version + 1 ?? 0;

            var result = await session.Events.AggregateStreamAsync(request.Id, state: agg, fromVersion: version, token: cancellationToken);

            return result != null 
                ? new(result.Name, result.Description, result.Medias)
                : default;
        }
    }
}
