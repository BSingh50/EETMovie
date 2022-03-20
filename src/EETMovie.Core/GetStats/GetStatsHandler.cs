using EETMovie.Core.Repository;
using EETMovie.Core.Repository.Entities;
using JetBrains.Annotations;
using MediatR;

namespace EETMovie.Core.GetStats;
using static GetStatsResponse.Factory;

[UsedImplicitly]
public class GetStatsHandler : IRequestHandler<GetStatsRequest, GetStatsResponse>
{
    private readonly IFileRepository _fileRepository;
    public GetStatsHandler(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }
    public async Task<GetStatsResponse> Handle(GetStatsRequest request,
                                         CancellationToken cancellationToken)
    {
        IEnumerable<Metadata> metadatas = await _fileRepository.GetMetadatas(cancellationToken);
        IEnumerable<Stats> stats = await _fileRepository.GetStats(cancellationToken);
        List<MovieStatistics> getStatsResponses = (from metadata in metadatas
                                                   group metadata by metadata.MovieId into movies
                                                      let watchDurationMsList = stats.Where(stat => stat.MovieId == movies.Key)
                                                                                     .Select(stat => stat.WatchDurationMs)
                                                                                     .ToList()
                                                      select new MovieStatistics
                                                             {
                                                                 MovieId = movies.FirstOrDefault()!.MovieId,
                                                                 Title = movies.FirstOrDefault()!.Title,
                                                                 ReleaseYear = movies.FirstOrDefault()!.ReleaseYear,
                                                                 Watches = watchDurationMsList.Count,
                                                                 AverageWatchDurationS = TimeSpan.FromMilliseconds(watchDurationMsList.Sum(Convert.ToInt64)).Seconds / watchDurationMsList.Count
                                                             }).ToList();

        return CreateSuccessResponse(getStatsResponses.OrderByDescending(stat => stat.Watches).ThenBy(stat => stat.ReleaseYear));
    }
}