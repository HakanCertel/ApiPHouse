using YayinEviApi.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using YayinEviApi.Domain.Entities.HubMessagesE;

namespace YayinEviApi.SignalR
{
    public static class HubRegistration
    {
        public static void MapHubs(this WebApplication webApplication)
        {
            webApplication.MapHub<ProductHub>("/products-hub");
            webApplication.MapHub<OrderHub>("/orders-hub");
            webApplication.MapHub<RecivedHubMessages>("/hub-message");
        }
    }
}
