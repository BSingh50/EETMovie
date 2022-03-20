using EETMovie.Core.Repository.Entities;

namespace EETMovie.Core.Repository;

public interface IFileRepository
{
    Task<string> CreateMetadataAsync();

    Task<Metadata> GetMetadataByMovieId(int movieId,
                                        CancellationToken cancellationToken);
    Task<IEnumerable<Metadata>> GetMetadatas(CancellationToken cancellationToken);
    Task<IEnumerable<Stats>> GetStats(CancellationToken cancellationToken);
}