
using YayinEviApi.Application.DTOs;
using YayinEviApi.Domain.Entities.Identity;

namespace YayinEviApi.Application.Abstractions.IToken
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(int second,AppUser user);
        string CreateRefreshToken();
    }
}
