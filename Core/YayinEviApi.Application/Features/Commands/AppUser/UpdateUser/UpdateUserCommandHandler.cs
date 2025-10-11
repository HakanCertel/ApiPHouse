using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Application.Abstractions.Hubs;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.Features.Commands.AppUser.CreateUser;

namespace YayinEviApi.Application.Features.Commands.AppUser.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, UpdateUserCommandResponse>
    {
        readonly IUserService _userService;
        readonly IProductHubService _productHubService;
        public UpdateUserCommandHandler(IUserService userService, IProductHubService productHubService)
        {
            _userService = userService;
            _productHubService = productHubService;
        }
        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUserResponse response = await _userService.UpdateUserAsync(new()
            {
                UserId= request.UserId,
                Email = request.Email,
                NameSurname = request.NameSurname,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm,
                Username = request.Username,
                DepartmentId = request.DepartmentId,
            });
            return new()
            {
                Message = response.Message,
                Succeeded = response.Succeeded,
            };

            //throw new UserCreateFailedException();
        }
    }
}
