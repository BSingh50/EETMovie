using System.Net;
using EETMovie.Core.Abstract;
using JetBrains.Annotations;

namespace EETMovie.Core.GetMetadata;

public class GetMetadataResponse : ResponseBase
{
    private GetMetadataResponse(HttpStatusCode httpStatusCode, string errorMessage)
    {
        HttpStatusCode = httpStatusCode;
        ErrorMessage = errorMessage;
        Metadata = null;
    }

    private GetMetadataResponse(MetadataResponse metadata)
    {
        HttpStatusCode = HttpStatusCode.Created;
        ErrorMessage = string.Empty;
        Metadata = metadata;
    }
    
    [UsedImplicitly]
    public MetadataResponse Metadata { get; }

    public static class Factory
    {
        public static GetMetadataResponse CreateSuccessResponse(MetadataResponse metadataResponse)
        {
            return new GetMetadataResponse(metadataResponse);
        }
        
        public static GetMetadataResponse CreateErrorResponse(HttpStatusCode httpStatusCode, string errorMessage)
        {
            return new GetMetadataResponse(httpStatusCode, errorMessage);
        }
    }
}