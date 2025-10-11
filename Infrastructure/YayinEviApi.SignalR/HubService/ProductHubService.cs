using Microsoft.AspNetCore.SignalR;
using YayinEviApi.Application.Abstractions.Hubs;
using YayinEviApi.SignalR.Hubs;

namespace YayinEviApi.SignalR.HubServices
{
    public class ProductHubService : IProductHubService
    {
        readonly IHubContext<ProductHub> _hubContext;

        public ProductHubService(IHubContext<ProductHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task ProductAddedMessageAsync(string message)
        {
            await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.ProductAddedMessage, message);
        }
    }
}
