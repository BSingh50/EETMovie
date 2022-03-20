using EETMovie.Core.Repository;
using JetBrains.Annotations;
using MediatR;

namespace EETMovie.Core.CreateMetadata;
using static CreateMetadataResponse.Factory;

[UsedImplicitly]
internal class CreateMetadataHandler : IRequestHandler<CreateMetadataRequest, CreateMetadataResponse>
{
    private readonly IFileRepository _fileRepository;

    public CreateMetadataHandler(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }
    public async Task<CreateMetadataResponse> Handle(CreateMetadataRequest request,
                                               CancellationToken cancellationToken)
    {
        string successMessage = await _fileRepository.CreateMetadataAsync();
        return CreateSuccessResponse(successMessage);
    }
}