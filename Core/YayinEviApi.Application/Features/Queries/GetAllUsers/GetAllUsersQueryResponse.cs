using YayinEviApi.Application.DTOs.User;

namespace YayinEviApi.Application.Features.Queries.AppUser.GetAllUsers
{
    public class GetAllUsersQueryResponse
    {
        public List<CreateUser> Users { get; set; }
        public int TotalUsersCount { get; set; }
    }
}