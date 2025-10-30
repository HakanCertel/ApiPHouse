using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using YayinEviApi.Application.DTOs.AgencyDto;
using YayinEviApi.Application.Repositories.AgencyR;
using YayinEviApi.Domain.Entities.AgencyE;

namespace YayinEviApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class AgencyConnectionInformationController : ControllerBase
    {
        readonly IAgencyConnectionInformationReadRepository _agencyConnectionInformationReadRepository;
        readonly IAgencyConnectionInformationWriteRepository _agencyConnectionInformationWriteRepository;

        public AgencyConnectionInformationController(IAgencyConnectionInformationReadRepository agencyConnectionInformationReadRepository, IAgencyConnectionInformationWriteRepository agencyConnectionInformationWriteRepository)
        {
            _agencyConnectionInformationReadRepository = agencyConnectionInformationReadRepository;
            _agencyConnectionInformationWriteRepository = agencyConnectionInformationWriteRepository;
        }

        [HttpPost()]
        public async Task<IActionResult> CretaeAgency(AgencyConnectionInformation[] agencyIngormation)
        {

            await _agencyConnectionInformationWriteRepository.AddRangeAsync(agencyIngormation.ToList());
            await _agencyConnectionInformationWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpGet()]
        public async Task<IActionResult> GetAllAgency()
        {
            var agencyL = _agencyConnectionInformationReadRepository.Select(null, x => new AgencyConnectionInformationL
            {
               Id = x.Id,
               AgencyId = x.AgencyId,
               IsDefault = x.IsDefault,
               Email = x.Email,
               NameSurname = x.NameSurname,
               PhoneNumber = x.PhoneNumber,
               Position = x.Position,
            });
            return Ok(agencyL);

        }
    }
}
