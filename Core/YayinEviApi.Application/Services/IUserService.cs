using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Domain.Entities.Identity;

namespace YayinEviApi.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);

        Task<CreateUserResponse> UpdateUserAsync(CreateUser model);
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
        //Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
        Task<List<CreateUser>> GetAllUsersAsync();
        Task<CreateUser> GetUser(string? id=null);
        int TotalUsersCount { get; }
        //Task AssignRoleToUserAsnyc(string userId, string[] roles);
        //Task<string[]> GetRolesToUserAsync(string userIdOrName);
        //Task<bool> HasRolePermissionToEndpointAsync(string name, string code);
    }
}
