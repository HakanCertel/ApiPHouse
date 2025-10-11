using MediatR;

namespace YayinEviApi.Application.Features.Commands.AppUser.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandRequest : IRequest<RefreshTokenLoginCommandResponse>
    {
        public string RefreshToken { get; set; }
    }
}
