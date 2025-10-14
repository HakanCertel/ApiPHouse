using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using YayinEviApi.Application.DTOs.HelperEntityDtos;
using YayinEviApi.Application.Repositories.IProccessCategoryR;
using YayinEviApi.Application.Repositories.IProccessR;
using YayinEviApi.Domain.Entities.HelperEntities.ProccessE;
using YayinEviApi.Domain.Entities.Identity;
using YayinEviApi.Infrastructure.Operations;
using YayinEviApi.Persistence.Repositories.ProccessR;

namespace YayinEviApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ProccessCategoryController : ControllerBase
    {
        readonly IProccessCategoryReadRepository _proccessCategoryReadRepository;

        readonly IProccessCategoryWriteRepository _proccessCategoryWriteRepository;
        
        readonly IHttpContextAccessor _httpContextAccessor;

        readonly UserManager<AppUser> _userManager;

        private string _username;
        public ProccessCategoryController(IProccessCategoryWriteRepository proccessCategoryWriteRepository, IProccessCategoryReadRepository proccessCategoryReadRepository, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _proccessCategoryWriteRepository = proccessCategoryWriteRepository;
            _proccessCategoryReadRepository = proccessCategoryReadRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _username = _httpContextAccessor.UserName();
            
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var proccessCategory = _proccessCategoryReadRepository.Select(null, x => new ProccessCategoryDto
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Code = x.Code,
                IsActive=x.IsActive,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                CreatingUserId=x.CreatingUserId,
                UpdatingUserId=x.UpdatingUserId,
            }).ToList();

            return Ok(proccessCategory);

        }

        [HttpGet("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetById(string id)
        {
            var proccessCategory = await _proccessCategoryReadRepository.GetByIdAsync(id);
            var newProccessCategory = new ProccessCategoryDto
            {
                Id = proccessCategory.Id.ToString(),
                Code = proccessCategory.Code ?? "",
                Name = proccessCategory.Name ?? "",
                IsActive= proccessCategory.IsActive,
            };

            return Ok(newProccessCategory);
        }

        [HttpPost()]
        public async Task<IActionResult> Add(ProccessCategoryDto proccessCategory)
        {
            var user = await _userManager.FindByNameAsync(_username);

            await _proccessCategoryWriteRepository.AddAsync(new()
            {
                Code = proccessCategory.Code,
                Name = proccessCategory.Name,
                IsActive= proccessCategory.IsActive,
                CreatingUserId=user?.Id,
            });
            await _proccessCategoryWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> Edit(ProccessCategoryDto proccessCategory)
        {
            var user = await _userManager.FindByNameAsync(_username);

            _proccessCategoryWriteRepository.Update(new()
            {
                Id =Guid.Parse(proccessCategory.Id),
                Code = proccessCategory.Code,
                Name = proccessCategory.Name,
                IsActive= proccessCategory.IsActive,
                UpdatingUserId=user.Id
            });
            await _proccessCategoryWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);

        }
        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            await _proccessCategoryWriteRepository.RemoveAsync(id);
            await _proccessCategoryWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
