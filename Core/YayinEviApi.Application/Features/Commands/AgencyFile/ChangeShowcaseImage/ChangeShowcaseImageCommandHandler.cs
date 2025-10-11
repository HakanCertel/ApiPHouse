using YayinEviApi.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using YayinEviApi.Application.Features.Commands.AuthorFile.ChangeShowcaseImage;
using YayinEviApi.Application.Repositories.IAuthotFileR;
using YayinEviApi.Application.Repositories.AgencyFileR;

namespace YayinEviApi.Application.Features.Commands.AgencyFile.ChangeShowcaseImage
{
    public class ChangeShowcaseImageCommandHandler : MediatR.IRequestHandler<ChangeShowcaseImageCommandRequest, ChangeShowcaseImageCommandResponse>
    {
        readonly IAgencyFileWriteRepository _agencyFileWriteRepository;

        public ChangeShowcaseImageCommandHandler(IAgencyFileWriteRepository agencyFileWriteRepository)
        {
            _agencyFileWriteRepository = agencyFileWriteRepository;
        }

        public async Task<ChangeShowcaseImageCommandResponse> Handle(ChangeShowcaseImageCommandRequest request, CancellationToken cancellationToken)
        {
            var query = _agencyFileWriteRepository.Table
                      .Include(p => p.Agencies)
                      .SelectMany(p => p.Agencies, (pif, p) => new
                      {
                          pif,
                          p
                      });

            var data = await query.FirstOrDefaultAsync(p => p.p.Id == Guid.Parse(request.Id) && p.pif.IsActive);

            if (data != null)
                data.pif.IsActive = false;

            var image = await query.FirstOrDefaultAsync(p => p.pif.Id == Guid.Parse(request.ImageId));
            if (image != null)
                image.pif.IsActive = true;

            await _agencyFileWriteRepository.SaveAsync();

            return new();
        }
    }
}
