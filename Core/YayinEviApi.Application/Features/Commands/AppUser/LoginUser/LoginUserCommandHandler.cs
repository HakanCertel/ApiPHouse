using MediatR;
using YayinEviApi.Application.Abstractions.Services;

namespace YayinEviApi.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IAuthService _authService;
        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password, 5000);
            if(token.AccessToken != null)
                return new LoginUserSuccessCommandResponse()
                {
                    Token = token,
                    UsernameOrEmail=request.UsernameOrEmail,
                };
            return new LoginUserErrorCommandResponse() { Message="Kullanıcı Bilgisi Hatası"};
        }
        // readonly IAuthService _authService;
        //public LoginUserCommandHandler(IAuthService authService)
        //{
        //    _authService = authService;
        //}
        //readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        //readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
        //readonly ITokenHandler _tokenHandler;

        //public LoginUserCommandHandler(
        //    UserManager<Domain.Entities.Identity.AppUser> userManager,
        //    SignInManager<Domain.Entities.Identity.AppUser> signInManager,
        //    ITokenHandler tokenHandler)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //    _tokenHandler = tokenHandler;
        //}
        //public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        //{
        //    Domain.Entities.Identity.AppUser user=await _userManager.FindByNameAsync(request.UsernameOrEmail);
        //    if (user==null) 
        //        user= await _userManager.FindByEmailAsync(request.UsernameOrEmail);
        //    if (user == null)
        //        throw new NotFoundUserException();

        //    SignInResult result=await _signInManager.CheckPasswordSignInAsync(user, request.Password,false);

        //    if (result.Succeeded) //Authentication başarılı
        //    {
        //        //Yetkilerin Belirlenmesi gerekiyor...
        //        Token token = _tokenHandler.CreateAccessToken(5);

        //        return new LoginUserSuccessCommandResponse()
        //        {
        //            Token = token
        //        };

        //    }

        //    throw new AuthenticationErrorException();
        //    //return new LoginUserErrorCommandResponse()
        //    //{
        //    //    Message="Kullanıcı Adı veya Şifre Hatalı..."
        //    //};
        //    //var token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password, 900);

        //}
    }
}