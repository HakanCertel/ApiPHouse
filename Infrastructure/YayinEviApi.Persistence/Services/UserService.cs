using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.Exceptions;
using YayinEviApi.Application.Repositories.IHelperEntitiesR.IDepartmentR;
using YayinEviApi.Domain.Entities.Identity;

namespace YayinEviApi.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IDepartmentReadRepository _departmentReadRepository;
        // readonly IEndpointReadRepository _endpointReadRepository;

        public UserService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor,IDepartmentReadRepository departmentReadRepository)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _departmentReadRepository = departmentReadRepository;
            //_endpointReadRepository = endpointReadRepository;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Email = model.Email,
                NameSurname = model.NameSurname,
            }, model.Password);

            CreateUserResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla oluşturulmuştur.";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
        }
        public async Task<CreateUserResponse> UpdateUserAsync(CreateUser user)
        {
            AppUser _user =await _userManager.FindByIdAsync(user.UserId);
            _user.NameSurname = user.NameSurname;
            _user.UserName = user.Username;
            _user.Email = user.Email;
            _user.DepartmentId= user.DepartmentId!=null? Guid.Parse( user.DepartmentId):null;
            
            IdentityResult result= await _userManager.UpdateAsync(_user);

            CreateUserResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla güncellenmiştir.";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
        }
        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new NotFoundUserException();
        }

        //public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        //{
        //    AppUser user = await _userManager.FindByIdAsync(userId);
        //    if (user != null)
        //    {
        //        resetToken = resetToken.UrlDecode();
        //        IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
        //        if (result.Succeeded)
        //            await _userManager.UpdateSecurityStampAsync(user);
        //        else
        //            throw new PasswordChangeFailedException();
        //    }
        //}

        public async Task<List<CreateUser>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            //.Skip(page * size)
            //.Take(size)
            //.ToListAsync();
            List<CreateUser> list = new List<CreateUser>();
            
            users.ForEach(x =>
            {
                var dd = new CreateUser
                {
                    UserId = x.Id,
                    Email=x.Email,
                    NameSurname=x.NameSurname,
                    Username=x.UserName,
                    DepartmentName= _departmentReadRepository.FindAsync(y=>y.Id==x.DepartmentId,x=>x).Result?.Name,
                };
                list.Add(dd);
            });
            
            return list;
            //return users.ForEach(user => new CreateUser
            //{
            //    UserId = user.Id,
            //    Email = user.Email,
            //    NameSurname = user.NameSurname,
            //    TwoFactorEnabled = user.TwoFactorEnabled,
            //    Username = user.UserName
            //}).ToList();
        }

        public async Task<CreateUser> GetUser(string? id)
        {
            AppUser user=new AppUser();
            
            if(id != null)
                user = await _userManager.FindByIdAsync(id);
            else
            {
                var userName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
                user=  await _userManager.FindByNameAsync(userName);
            }
            
            var _user = new CreateUser
            {
                UserId=user?.Id,
                Username=user?.UserName,
                NameSurname=user?.NameSurname,
                DepartmentId=user?.DepartmentId?.ToString(),
                DepartmentName=user?.Department?.Name,
                Email=user?.Email,
            };
            return _user;
        }
        public int TotalUsersCount => _userManager.Users.Count();



        //public async Task AssignRoleToUserAsnyc(string userId, string[] roles)
        //{
        //    AppUser user = await _userManager.FindByIdAsync(userId);
        //    if (user != null)
        //    {
        //        var userRoles = await _userManager.GetRolesAsync(user);
        //        await _userManager.RemoveFromRolesAsync(user, userRoles);

        //        await _userManager.AddToRolesAsync(user, roles);
        //    }
        //}
        //public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
        //{
        //    AppUser user = await _userManager.FindByIdAsync(userIdOrName);
        //    if (user == null)
        //        user = await _userManager.FindByNameAsync(userIdOrName);

        //    if (user != null)
        //    {
        //        var userRoles = await _userManager.GetRolesAsync(user);
        //        return userRoles.ToArray();
        //    }
        //    return new string[] { };
        //}

        //public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
        //{
        //    var userRoles = await GetRolesToUserAsync(name);

        //    if (!userRoles.Any())
        //        return false;

        //    Endpoint? endpoint = await _endpointReadRepository.Table
        //             .Include(e => e.Roles)
        //             .FirstOrDefaultAsync(e => e.Code == code);

        //    if (endpoint == null)
        //        return false;

        //    var hasRole = false;
        //    var endpointRoles = endpoint.Roles.Select(r => r.Name);

        //    //foreach (var userRole in userRoles)
        //    //{
        //    //    if (!hasRole)
        //    //    {
        //    //        foreach (var endpointRole in endpointRoles)
        //    //            if (userRole == endpointRole)
        //    //            {
        //    //                hasRole = true;
        //    //                break;
        //    //            }
        //    //    }
        //    //    else
        //    //        break;
        //    //}

        //    //return hasRole;

        //    foreach (var userRole in userRoles)
        //    {
        //        foreach (var endpointRole in endpointRoles)
        //            if (userRole == endpointRole)
        //                return true;
        //    }

        //    return false;
        //}
    }
}
