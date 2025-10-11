using Microsoft.EntityFrameworkCore;
using YayinEviApi.Application.Repositories.IMaterialR;

namespace YayinEviApi.Application.Features.Commands.MaterailFile.ChangeShowcaseImage
{
    public class ChangeShowcaseImageCommandHandler : MediatR.IRequestHandler<ChangeShowcaseImageCommandRequest, ChangeShowcaseImageCommandResponse>
    {
        readonly IMaterialFileRepository _materialFileRepository;

        public ChangeShowcaseImageCommandHandler( IMaterialFileRepository materialFileRepository)
        {
            _materialFileRepository = materialFileRepository;
        }

        public async Task<ChangeShowcaseImageCommandResponse> Handle(ChangeShowcaseImageCommandRequest request, CancellationToken cancellationToken)
        {
            var query = _materialFileRepository.Table
                      .Include(p => p.Materials)
                      .SelectMany(p => p.Materials, (pif, p) => new
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

            await _materialFileRepository.SaveAsync();

            return new();
        }
    }
}
