using System.Net;
using EETMovie.Core.Abstract;

namespace EETMovie.Core.CreateMetadata;

public class CreateMetadataResponse : ResponseBase
{
    private CreateMetadataResponse(string message)
    {
        HttpStatusCode = HttpStatusCode.Created;
        ErrorMessage = string.Empty;
        SuccessMessage = message;
    }
    
    public string SuccessMessage { get; }

    public static class Factory
    {
        public static CreateMetadataResponse CreateSuccessResponse(string successMessage)
        {
            return new CreateMetadataResponse(successMessage);
        }
    }
}