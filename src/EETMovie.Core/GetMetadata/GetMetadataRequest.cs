using MediatR;

namespace EETMovie.Core.GetMetadata;

public class GetMetadataRequest : IRequest<GetMetadataResponse>
{
    public GetMetadataRequest(int movieId)
    {
        MovieId = movieId;
    }
    public int MovieId { get; }
}