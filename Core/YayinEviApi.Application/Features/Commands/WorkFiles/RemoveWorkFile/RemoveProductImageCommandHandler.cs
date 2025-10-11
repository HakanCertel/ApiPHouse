using MediatR;
using Microsoft.EntityFrameworkCore;
using YayinEviApi.Application.Features.Commands.WorkFiles.RemoveWorkFile;
using YayinEviApi.Domain.Entities.WorkE;
using YayinEviApi.Application.Repositories.IWorkR;
using YayinEviApi.Domain.Entities;

namespace YayinEviApi.Application.Features.Commands.WorkFiles.RemoveProductWorkFile
{
    public class RemoveWorkFileCommandHandler : IRequestHandler<RemoveWorkFileCommandRequest, RemoveWorkFileCommandResponse>
    {

        readonly IWorkReadRepository _workReadRepository;
        readonly IWorkWriteRepository _workWriteRepository;

        public RemoveWorkFileCommandHandler(IWorkReadRepository workReadRepository, IWorkWriteRepository workWriteRepository)
        {
            _workReadRepository = workReadRepository;
            _workWriteRepository = workWriteRepository;
        }

        public async Task<RemoveWorkFileCommandResponse> Handle(RemoveWorkFileCommandRequest request, CancellationToken cancellationToken)
        {
            Work? work = await _workReadRepository.Table.Include(p => p.PublishFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

            PublishFile? publishFile = work?.PublishFiles.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));

            if (publishFile != null)
                work?.PublishFiles.Remove(publishFile);

            await _workWriteRepository.SaveAsync();
            return new();
        }
    }
}
