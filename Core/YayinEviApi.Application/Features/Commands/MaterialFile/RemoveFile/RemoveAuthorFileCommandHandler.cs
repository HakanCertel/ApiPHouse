using MediatR;
using Microsoft.EntityFrameworkCore;
using YayinEviApi.Application.Repositories.IMaterialR;
using YayinEviApi.Domain.Entities.MaterialE;

namespace YayinEviApi.Application.Features.Commands.MaterailFile.RemoveFile
{
    public class RemoveFileCommandHandler : IRequestHandler<RemoveFileCommandRequest, RemoveFileCommandResponse>
    {

        readonly IMaterialRepository _materialRepository;
        public RemoveFileCommandHandler(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public async Task<RemoveFileCommandResponse> Handle(RemoveFileCommandRequest request, CancellationToken cancellationToken)
        {
            Material? material = await _materialRepository.Table.Include(p => p.MaterialFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

            MaterialFile? materialFile = material?.MaterialFiles.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));

            if (materialFile != null)
                material?.MaterialFiles.Remove(materialFile);

            await _materialRepository.SaveAsync();
            return new();
        }
    }
}
