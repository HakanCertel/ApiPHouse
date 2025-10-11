using Microsoft.Extensions.DependencyInjection;
using YayinEviApi.Application.Abstractions.Hubs;
using YayinEviApi.SignalR.HubService;
using YayinEviApi.SignalR.HubServices;

namespace YayinEviApi.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection collection)
        {
            collection.AddTransient<IProductHubService, ProductHubService>();
            collection.AddTransient<IOrderHubService, OrderHubService>();
            collection.AddTransient<IHubMessage, RecievedHubMessageService>();
            collection.AddSignalR();
        }
    }
}
