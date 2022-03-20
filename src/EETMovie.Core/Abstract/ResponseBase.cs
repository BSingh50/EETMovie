using System.Net;
using JetBrains.Annotations;

namespace EETMovie.Core.Abstract;

[PublicAPI]
public abstract class ResponseBase
{
    public HttpStatusCode HttpStatusCode { get; protected set; }
    public string ErrorMessage { get; protected set; }
}