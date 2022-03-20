using EETMovie.Core.Repository.Entities;
using JetBrains.Annotations;

namespace EETMovie.Core.GetMetadata;

[PublicAPI]
public class MetadataResponse
{
    public MetadataResponse(Metadata metadata)
    {
        MovieId = metadata.MovieId;
        Title = metadata.Title;
        Language = metadata.Language;
        Duration = metadata.Duration;
        ReleaseYear = metadata.ReleaseYear;
    }
    
    public int MovieId { get; }
    public string Title { get; }
    public string Language { get; }
    public string Duration { get; }
    public int ReleaseYear { get; }
}