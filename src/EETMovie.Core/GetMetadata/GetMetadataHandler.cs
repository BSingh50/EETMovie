using System.Net;
using EETMovie.Core.Repository;
using EETMovie.Core.Repository.Entities;
using JetBrains.Annotations;
using MediatR;

namespace EETMovie.Core.GetMetadata;
using static GetMetadataResponse.Factory;

[UsedImplicitly]
public class GetMetadataHandler : IRequestHandler<GetMetadataRequest, GetMetadataResponse>
{
    private readonly IFileRepository _fileRepository;
    public GetMetadataHandler(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }
    public async Task<GetMetadataResponse> Handle(GetMetadataRequest request,
                                            CancellationToken cancellationToken)
    {
        if (FileRepository.Database.Count == 0)
        {
            return CreateErrorResponse(HttpStatusCode.NotFound, "No metadata found");
        }
        
        Metadata metadata = await _fileRepository.GetMetadataByMovieId(request.MovieId, cancellationToken);
        
        return CreateSuccessResponse(new MetadataResponse(metadata));
    }
}