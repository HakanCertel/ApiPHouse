using YayinEviApi.Application.DTOs.RezervationDtos;

namespace YayinEviApi.Application.Services
{
    public interface IRezervationService
    {
        public Task<RezervationDto>? GetSingle(string id);
        public Task<List<RezervationDto>> GertAll();
    }
}
