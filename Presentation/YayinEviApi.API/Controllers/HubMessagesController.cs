using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using YayinEviApi.Application.Abstractions.Hubs;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.DTOs.HelperEntityDtos;
using YayinEviApi.Application.DTOs.HubMessagesDtos;
using YayinEviApi.Application.DTOs.User;
using YayinEviApi.Application.DTOs.WorkOrderDtos;
using YayinEviApi.Application.Repositories.HubMessagesR;
using YayinEviApi.Application.Repositories.IWorkOrderR;
using YayinEviApi.Domain.Entities.HubMessagesE;
using YayinEviApi.Domain.Entities.Identity;
using YayinEviApi.Persistence.Repositories.WorkOrderR;

namespace YayinEviApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class HubMessagesController : ControllerBase
    {
        readonly IHubMessageReadRepository _hubMessageReadRepository;
        readonly IHubMessageWriteRepository _hubMessageWriteRepository;
        readonly IWorkOrderReadRepository _workOrderReadRepository;
        readonly IUserService _userService;
        readonly UserManager<AppUser> _userManager;
        readonly IProductHubService _productHubService;
        readonly CreateUser _user;
        public HubMessagesController(IHubMessageWriteRepository hubMessageWriteRepository, IHubMessageReadRepository hubMessageReadRepository, IUserService userService, UserManager<AppUser> userManager, IWorkOrderReadRepository workOrderReadRepository, IProductHubService productHubService)
        {
            _hubMessageWriteRepository = hubMessageWriteRepository;
            _hubMessageReadRepository = hubMessageReadRepository;
            _userService = userService;
            _userManager = userManager;
            _workOrderReadRepository = workOrderReadRepository;

            _user = _userService.GetUser().Result;
            _productHubService = productHubService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var hubMessages = _hubMessageReadRepository.Table.Where(x=>x.SendedUserId==_user.UserId && x.IsShowed==true).Select(x=> new
            {
                hubMessage=x,
                //creatingUserName=_userManager.FindByIdAsync(x.CreatingUserId).Result.NameSurname,

            }).Select(x => new HubMessageDto
            {
                Id = x.hubMessage.Id.ToString(),
                CreatingUserId = x.hubMessage.CreatingUserId,
                CreatingUserNameSurname=x.hubMessage.CreatingUserNameSurname,
                SendedUserId=x.hubMessage.SendedUserId,
                IsShowed=x.hubMessage.IsShowed,
                Readed=x.hubMessage.Readed,
                MessageHead=x.hubMessage.MessageHead,
                MessageBody=x.hubMessage.MessageBody,
                CreatedDate = x.hubMessage.CreatedDate,
            }).ToList();

            return Ok(hubMessages);

        }
        [HttpPost()]
        public async Task<IActionResult> Add(IList<HubMessage> hubMessages)
        {
            //var user = await _userManager.FindByNameAsync(_username);

            await _hubMessageWriteRepository.AddRangeAsync(hubMessages.ToList());
            await _hubMessageWriteRepository.SaveAsync();

            await _productHubService.ProductAddedMessageAsync($"Yeni Bildiriminiz var...");
            
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(IList<HubMessageDto> hubMessages)
        {
            foreach (var item in hubMessages)
            {
                _hubMessageWriteRepository.Update(new()
                {
                    Id = Guid.Parse(item.Id),
                    CreatingUserId = item.CreatingUserId,
                    CreatingUserNameSurname = item.CreatingUserNameSurname,
                    SendedUserId = item.SendedUserId,
                    IsShowed = item.IsShowed,
                    Readed = true,
                    MessageHead = item.MessageHead,
                    MessageBody = item.MessageBody,
                    CreatedDate = item.CreatedDate,
                });
                item.Readed = true;
                
            }
            await _hubMessageWriteRepository.SaveAsync();

            await _productHubService.ProductAddedMessageAsync($"mesaj okundu olarak belirlendi.");

            return Ok(hubMessages);

        }
    }
}
