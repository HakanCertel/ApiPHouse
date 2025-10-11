using Microsoft.Extensions.DependencyInjection;
using MediatR;
using YayinEviApi.Application.Abstractions.Hubs;

namespace YayinEviApi.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection collection)
        {
            collection.AddMediatR(typeof(ServiceRegistration));
            collection.AddHttpClient();
        }
    }
}