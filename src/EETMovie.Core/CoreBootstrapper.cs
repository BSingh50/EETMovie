using EETMovie.Core.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EETMovie.Core;

public static class CoreBootstrapper
{
    public static void RegisterCoreDependencies(this IServiceCollection services)
    {
        services.AddMediatR(typeof(CoreBootstrapper))
                .AddTransient<IFileRepository, FileRepository>();
    }
}