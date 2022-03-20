using JetBrains.Annotations;
using MediatR;

namespace EETMovie.Core.CreateMetadata;

[PublicAPI]
public class CreateMetadataRequest : IRequest<CreateMetadataResponse>
{
    public int MovieId { get; set; }
    public string Title { get; set; }
    public string Language { get; set; }
    public string Duration { get; set; }
    public int ReleaseYear { get; set; }
}