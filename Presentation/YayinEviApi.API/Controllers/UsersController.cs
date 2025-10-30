using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.Abstractions.Storage;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.Features.Commands.AppUser.CreateUser;
using YayinEviApi.Application.Features.Commands.AppUser.UpdateUser;
using YayinEviApi.Application.Features.Queries.AppUser.GetAllUsers;
using YayinEviApi.Application.Repositories;
using YayinEviApi.Application.Repositories.IHelperEntitiesR.IDepartmentR;
using YayinEviApi.Domain.Entities;
using YayinEviApi.Domain.Entities.Identity;

namespace YayinEviApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Admin")]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly UserManager<AppUser> _userManager;
        readonly CreateUser _user;
        readonly IUserService _userService;
        private readonly IStorageService _storageService;
        readonly IDepartmentReadRepository _departmentReadRepository;
        readonly IFileManagementReadRepository _fileManagementReadRepository;
        readonly IFileManagementWriteRepository _fileManagementWriteRepository;
        public UsersController(IMediator mediator, UserManager<AppUser> userManager, IStorageService storageService, IDepartmentReadRepository departmentReadRepository, IFileManagementReadRepository fileManagementReadRepository, IFileManagementWriteRepository fileManagementWriteRepository, IUserService userService)
        {
            _mediator = mediator;
            _userManager = userManager;
            _storageService = storageService;
            _departmentReadRepository = departmentReadRepository;
            _fileManagementReadRepository = fileManagementReadRepository;
            _fileManagementWriteRepository = fileManagementWriteRepository;
            _userService = userService;

            //_user = _userService.GetUser().Result;
        }

        [HttpPost]
        public async Task <IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserCommandRequest updateUserCommandRequest)
        {
            UpdateUserCommandResponse response = await _mediator.Send(updateUserCommandRequest);
            return Ok(response);
        }
        
        [HttpGet]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryRequest getAllUsersQueryRequest)
        {
            GetAllUsersQueryResponse response = await _mediator.Send(getAllUsersQueryRequest);
            return Ok(new { response.TotalUsersCount, response.Users });
        }
        
        [HttpGet("[action]/{id}")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var departmentName =await _departmentReadRepository.FindAsync(x => x.Id == user.DepartmentId,x=>x );
            var imagePath=await _fileManagementReadRepository.FindAsync(x=>x.IsActive && x.EntityId==user.Id,x=>x);
            var newUser = new CreateUser
            {
                UserId = user?.Id,
                DepartmentId=user?.DepartmentId?.ToString(),
                DepartmentName= departmentName?.Name,
                Email=user?.Email,
                NameSurname=user?.NameSurname,
                Username=user?.UserName,
                ImagePath=imagePath?.Path
            };

            return Ok(newUser);
        }

        [HttpPost("[action]")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Upload(string entityId, string? whichPage)
        {

            var datas = await _storageService.UploadAsync($@"resorce\user-images", Request.Form.Files);

            //Work work = await _fileManagementReadRepository.GetByIdAsync(id);
                 await _fileManagementWriteRepository.AddRangeAsync(datas.Select(d => new FileManagement
            {
                FileName = d.filename,
                Path = d.pathOrContainerName,
                Storage = _storageService.StorageName,
                EntityId= entityId,
                WhichPage = whichPage,
                WhichClass ="AppUserClass",
                //AddingUserId=_user.UserId,

            }).ToList());

            await _fileManagementWriteRepository.SaveAsync();
            return Ok();

        }
        
        [HttpGet("[action]")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetFiles(string entityId)
        {
           var file= _fileManagementReadRepository.Select(x=>x.EntityId== entityId, x=>x);
           
            return Ok(file);
        }
        
        [HttpGet("[action]")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> ChangeShowcaseImage(string imageId,string id)
        {
            var files= _fileManagementReadRepository.Select(x=>x.EntityId== id, x=>x);
            var file1 = files.Where(x => x.IsActive).FirstOrDefault();
            if (file1 != null)
            {
                file1.IsActive = false;
            }
            var file2 = files.Where(x => x.EntityId==id&&x.Id.ToString()==imageId).FirstOrDefault();
            if(file2 != null)
            {
                file2.IsActive = true;
            }
            await _fileManagementWriteRepository.SaveAsync();
            
            return Ok();
        }
        //[HttpPost("[action]")]
        //public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        //{
        //    LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
        //    return Ok(response);
        //}
        //[HttpPost("google-login")]
        //public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest)
        //{
        //    GoogleLoginCommandResponse response = await _mediator.Send(googleLoginCommandRequest);
        //    return Ok(response);
        //}
    }
}
