using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Application.Abstractions.Hubs;
using YayinEviApi.Application.DTOs.HubMessagesDtos;
using YayinEviApi.SignalR.Hubs;

namespace YayinEviApi.SignalR.HubService
{
    public class RecievedHubMessageService : IHubMessage
    {
        readonly IHubContext<ProductHub> _hubContext;

        public RecievedHubMessageService(IHubContext<ProductHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task RecievedMessageAsync(IList<HubMessageDto> hubMessages)
        {
            await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.RecievedMessage, hubMessages);
        }
    }
}
