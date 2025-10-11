using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Domain.Entities.HubMessagesE;
using YayinEviApi.Domain.Entities.WorkOrderE;

namespace YayinEviApi.Infrastructure.Operations
{
    public static class CreateHubMessages
    {
        public static List<HubMessage> Create(string header,string body,CreateUser user,IList<WorkAssignedUsers> users)
        {
            List<HubMessage> messages=new List<HubMessage>();
            if(users != null || users?.Count != 0)
            {
                foreach (var item in users)
                {
                    if (item.UserId == user.UserId)
                        continue;
                   
                    var mes = new HubMessage
                    {
                        MessageHead = header,
                        MessageBody = body,
                        CreatingUserId=user.UserId,
                        SendedUserId=item.UserId,
                        IsShowed = true,
                    };
                    messages.Add(mes);
                }
            }
            return messages;
        }
    }
}
