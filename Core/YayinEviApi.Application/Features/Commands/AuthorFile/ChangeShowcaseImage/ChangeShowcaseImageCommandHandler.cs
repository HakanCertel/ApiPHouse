using YayinEviApi.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using YayinEviApi.Application.Features.Commands.AuthorFile.ChangeShowcaseImage;
using YayinEviApi.Application.Repositories.IAuthotFileR;

namespace YayinEviApi.Application.Features.Commands.AuthorFile.ChangeShowcaseImage
{
    public class ChangeShowcaseImageCommandHandler : MediatR.IRequestHandler<ChangeShowcaseImageCommandRequest, ChangeShowcaseImageCommandResponse>
    {
        readonly IAuthorFileWriteRepository _authorFileWriteRepository;

        public ChangeShowcaseImageCommandHandler( IAuthorFileWriteRepository authorFileWriteRepository)
        {
            _authorFileWriteRepository = authorFileWriteRepository;
        }

        public async Task<ChangeShowcaseImageCommandResponse> Handle(ChangeShowcaseImageCommandRequest request, CancellationToken cancellationToken)
        {
            var query = _authorFileWriteRepository.Table
                      .Include(p => p.Authors)
                      .SelectMany(p => p.Authors, (pif, p) => new
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

            await _authorFileWriteRepository.SaveAsync();

            return new();
        }
    }
}
