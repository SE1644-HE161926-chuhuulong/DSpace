using Application.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class AddExtension
{
    public static IServiceCollection AddExtensions(this IServiceCollection serviceCollection, IConfiguration config)
    {
        serviceCollection.AddAutoMapper(typeof(MappingProfiles));
        // serviceCollection.AddScoped<PeopleService, PeopleServiceImpl>();
        return serviceCollection;
    }
}