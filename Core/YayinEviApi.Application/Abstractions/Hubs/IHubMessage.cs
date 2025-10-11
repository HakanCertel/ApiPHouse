using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Application.DTOs.HubMessagesDtos;

namespace YayinEviApi.Application.Abstractions.Hubs
{
    public interface IHubMessage
    {

        Task RecievedMessageAsync(IList<HubMessageDto> hubMessages);

    }
}
