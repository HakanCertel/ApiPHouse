using MediatR;

namespace YayinEviApi.Application.Features.Commands.AppUser.UpdateUser
{
    public class UpdateUserCommandRequest : IRequest<UpdateUserCommandResponse>
    {
        public string? UserId { get; set; }
        public string NameSurname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? PasswordConfirm { get; set; }
        public string? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
    }
}
