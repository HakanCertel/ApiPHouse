using MediatR;
using YayinEviApi.Application.Abstractions.Services;

namespace YayinEviApi.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        readonly IAuthService _authService;

        public GoogleLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.GoogleLoginAsync(request.IdToken, 900);
            return new()
            {
                Token = token
            };
        }
        //readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        //readonly ITokenHandler _tokenHandler;
        //public GoogleLoginCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, ITokenHandler tokenHandler)
        //{
        //    _userManager = userManager;
        //    _tokenHandler = tokenHandler;
        //}

        ////readonly IAuthService _authService;

        ////public GoogleLoginCommandHandler(IAuthService authService)
        ////{
        ////    _authService = authService;
        ////}

        //public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        //{
        //    var setting = new GoogleJsonWebSignature.ValidationSettings()
        //    {
        //        Audience = new List<string>() { "747632565857-4bsqjssmbtmftmfte1fujfahn8lf8hrv.apps.googleusercontent.com" }
        //    };

        //    var payload=await GoogleJsonWebSignature.ValidateAsync(request.IdToken, setting);

        //    var info=new UserLoginInfo(request.Provider,payload.Subject,request.Provider);

        //    Domain.Entities.Identity.AppUser user=await _userManager.FindByLoginAsync(info.LoginProvider,info.ProviderKey);

        //    bool result= user!=null;

        //    if (user == null) {

        //        //user = await _userManager.FindByEmailAsync(payload.Email);

        //        if (user == null) {

        //            user = new()
        //            {
        //                Id = Guid.NewGuid().ToString(),
        //                Email = payload.Email,
        //                UserName = payload.Email,
        //                NameSurname = payload.Name
        //            };

        //            var identityResult=await _userManager.CreateAsync(user);

        //            result=identityResult.Succeeded;
        //        }
        //    }

        //    if (result)
        //        await _userManager.AddLoginAsync(user, info);
        //    else
        //        throw new Exception("invalid external authentication");
        //    Token token = _tokenHandler.CreateAccessToken(5);
        //    return new()
        //    {
        //        Token = token
        //    };
        //}
    }
}
