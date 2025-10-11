using MediatR;
using Microsoft.AspNetCore.Identity;
using YayinEviApi.Application.Abstractions.Hubs;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.User;

namespace YayinEviApi.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;
        readonly IProductHubService _productHubService;
        public CreateUserCommandHandler(IUserService userService, IProductHubService productHubService)
        {
            _userService = userService;
            _productHubService = productHubService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUserResponse response = await _userService.CreateAsync(new()
            {
                Email = request.Email,
                NameSurname = request.NameSurname,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm,
                Username = request.Username,
                DepartmentId=request.DepartmentId,
            });
            return new()
            {
                Message = response.Message,
                Succeeded = response.Succeeded,
            };

            //throw new UserCreateFailedException();
        }
        //readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        //public CreateUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
        //{
        //    _userManager = userManager;
        //}

        //public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        //{
        //    IdentityResult result = await _userManager.CreateAsync(new()
        //    {
        //        Id=Guid.NewGuid().ToString(),
        //        UserName=request.Username,
        //        Email = request.Email,
        //        NameSurname = request.NameSurname,
        //    },request.Password);

        //    CreateUserCommandResponse response = new() { Succeeded=result.Succeeded};

        //    if (result.Succeeded) 
        //        response.Message = "Kullanıcı Başarıyla Oluşturulmuştur.";
        //    else
        //        foreach (var error in result.Errors)
        //        {
        //            response.Message += $"{error.Code}-{error.Description}<br>";
        //        }

        //    return response;
        //    //return new()
        //    //{
        //    //    Message = response.Message,
        //    //    Succeeded = response.Succeeded,
        //    //};

        //    //throw new UserCreateFailedException();

        //}
    }
}
